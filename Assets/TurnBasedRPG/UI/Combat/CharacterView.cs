using TurnBasedRPG.BattleStats;
using TurnBasedRPG.StatusEffects;
using TurnBasedRPG.UI.Views;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedRPG.UI.Combat {
    public class CharacterView : DataView<Character> {
        [SerializeField] private Image portrait;
        [SerializeField] private GameObject shieldIcon;
        [SerializeField] private ResourceStatView healthView;
        [SerializeField] private ResourceStatView shieldView;
        [SerializeField] private ResourceStatView manaView;
        [SerializeField] private Image speedView;
        [SerializeField] private GameObject turnArrow;
        [SerializeField] private StatusEffectListView statusEffectView;

        public Color normalSpeedColor;
        public Color fastSpeedColor;
        public Color slowSpeedColor;

        protected override void Subscribe() {
            Data.OnStartTurn += ActivateArrow;
            Data.OnEndTurn += DeactivateArrow;
            Data.Health.OnChanged += ChangePortraitAlpha;
            Data.Shield.OnChanged += ToggleShield;
            Data.Stats.Speed.OnChanged += ChangeSpeedColor;
            Data.actionPoints.OnChange += UpdateSpeedSlider;
            Data.StatusEffectList.OnStatusAdded += HandleNewStat;

            healthView.SetData(Data.Health);
            shieldView.SetData(Data.Shield);
            manaView.SetData(Data.Mana);
            statusEffectView.SetData(Data.StatusEffectList);

            ChangeSpeedColor(Data[Stat.Speed]);
            turnArrow.SetActive(false);
        }

        protected override void Unsubscribe() {
            Data.Health.OnChanged -= ChangePortraitAlpha;
            Data.Shield.OnChanged -= ToggleShield;
            Data.Stats.Speed.OnChanged -= ChangeSpeedColor;
            Data.actionPoints.OnChange -= UpdateSpeedSlider;
            Data.StatusEffectList.OnStatusAdded -= HandleNewStat;

            healthView.SetData(null);
            shieldView.SetData(null);
            manaView.SetData(null);
            statusEffectView.SetData(null);
        }

        private void ToggleShield(ResourceStat stat, int delta) {
            shieldIcon.SetActive(stat.Current > 0);
        }

        private void ActivateArrow(Character _) => turnArrow.SetActive(true);
        private void DeactivateArrow(Character _) => turnArrow.SetActive(false);

        private void ChangeSpeedColor(int value) {
            if(!speedView) return;
            var baseValue = Data.Stats.Speed.Base;
            var color = value == baseValue ? normalSpeedColor : value > baseValue ? fastSpeedColor : slowSpeedColor;
            speedView.color = color;
        }

        private void UpdateSpeedSlider(float value) {
            speedView.fillAmount = value / Data.cm.MaxSpeed;
        }

        private void ChangePortraitAlpha(ResourceStat stat, int _) {
            portrait.color = stat.Current == 0 ? new Color(1, 1, 1, 0.5f) : Color.white;
        }

        private void HandleNewStat(Status status) {
            var color = status.Source.TextColor;
            var dName = status.Source.DisplayName;
            var pos = Data.cm.View.transform;
            var statusPopup = Data.cm.View.callPopup.Pop(dName, color, transform.position, 0.5f);
            Data.cm.CombatEvents.Enqueue(statusPopup);
        }
    }
}