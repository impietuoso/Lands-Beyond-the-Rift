using TurnBasedRPG.BattleStats;
using TurnBasedRPG.StatusEffect;
using UnityEngine;

namespace TurnBasedRPG.UI_Review {
    [RequireComponent(typeof(Character))]
    public class CharacterFeedbacks : DataView<Character> {
        [SerializeField] private ResourceStatView healthView;
        [SerializeField] private GameObject turnMarker;
        [SerializeField] private GameObject targetMark;
        private Animator _animator;

        public GameObject TargetMark => targetMark;

        public void SelectAsTarget() => Data.cm.selectedTarget = Data;

        protected override void Subscribe() {
            Data.PlayAnimation += _animator.Play;
            Data.OnResolveDefend += DamagePopup;
            Data.StatusEffectList.OnStatusAdded += StatusMessage;
            Data.OnStartTurn += ActivateTurnMarker;
            Data.OnEndTurn += DeactivateTurnMarker;

            healthView.SetData(Data.Health);
            _animator = Instantiate(Data.member.prefab, transform);
            _animator.Play("Spawn");
        }

        protected override void Unsubscribe() {
            Data.PlayAnimation -= _animator.Play;
            Data.OnResolveDefend -= DamagePopup;
            Data.StatusEffectList.OnStatusAdded -= StatusMessage;
            Data.OnStartTurn -= ActivateTurnMarker;
            Data.OnEndTurn -= DeactivateTurnMarker;
        }

        private void ActivateTurnMarker(Character _) => turnMarker.SetActive(true);
        private void DeactivateTurnMarker(Character obj) => turnMarker.SetActive(false);

        private void StatusMessage(Status status) {
            var popup = Data.cm.combatUI.callPopup;
            var color = status.source.statusPopupColor;
            StartCoroutine(popup.Pop(status.statusName, color, transform, 0));
        }

        private void DamagePopup(CombatArgs args) {
            var damageColor = args.result switch
            {
                { Miss: true } => Color.white,
                { ResistStatus: true } => Color.orange,
                { Crit: true } => Color.yellow,
                { Health: { Delta: > 0 } } => Color.green,
                { Health: { Delta: < 0 } } => Color.red,
                { Mana: { Delta: > 0 } } => Color.blue,
                { Mana: { Delta: < 0 } } => Color.blueViolet,
                { Shield: { Delta: > 0 } } => Color.cyan,
                { Shield: { Delta: < 0 } } => Color.gray3,
                _ => Color.deepPink
            };

            var statusApplied = args.statusEffects.Count > 0 && !args.result.ResistStatus;
            if(statusApplied && args.damage == 0) return;

            var popupValue = args.result.Shield.Delta + args.result.Health.Delta;
            if(args.user == args.target && popupValue == 0) return;

            var popupText = args.result.Miss ? "Miss" : popupValue.ToString();
            var popup = Data.cm.combatUI.callPopup;

            if(!args.result.Miss && args.result.ResistStatus) {
                if(popupValue == 0)
                    popupText = "Resist";
                else
                    popupText += "\nResist";
            }

            StartCoroutine(popup.Pop(popupText, damageColor, transform, 0));
            if(args.result.IsFatal) _animator.Play("Die", 0, 0);
            if(args.result.IsRevive) _animator.Play("Spawn", 0, 0);
        }
    }
}