using TurnBasedRPG.BattleStats;
using TurnBasedRPG.StatusEffect;
using TurnBasedRPG.UI.Views;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedRPG.UI {
    public class CharacterView : DataView<Character> {
        [SerializeField] private Image portrait;
        [SerializeField] private GameObject shieldIcon;
        [SerializeField] private ResourceStatView healthView;
        [SerializeField] private ResourceStatView shieldView;
        [SerializeField] private ResourceStatView manaView;
        [SerializeField] private Image speedView;
        [SerializeField] private StatusEffectListView statusEffectView;

        public Color normalSpeedColor;
        public Color fastSpeedColor;
        public Color slowSpeedColor;

        protected override void Subscribe() {
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

        private void ChangeSpeedColor(int value) {
            if(!speedView) return;
            var baseValue = Data.Stats.Speed.Base;
            var color = value == baseValue ? normalSpeedColor : value > baseValue ? fastSpeedColor : slowSpeedColor;
            speedView.color = color;
        }

        private void UpdateSpeedSlider(float value) {
            speedView.fillAmount = value / Data.cm.maxSpeed;
        }

        private void ChangePortraitAlpha(ResourceStat stat, int _) {
            portrait.color = stat.Current == 0 ? new Color(1, 1, 1, 0.5f) : Color.white;
        }

        private void HandleNewStat(Status newStatus) {
            var color = newStatus.source.statusPopupColor;
            var statusPopup = Data.cm.combatUI.callPopup.Pop(newStatus.statusName, color, transform, 0);
            var genericEvent = new GenericCombatEvent(statusPopup);
            CombatManager.instance.combatEvents.Enqueue(genericEvent);
        }
    }
}