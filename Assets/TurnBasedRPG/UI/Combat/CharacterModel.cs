using TurnBasedRPG.BattleStats;
using TurnBasedRPG.StatusEffects;
using UnityEngine;

namespace TurnBasedRPG.UI.Combat {
    public class CharacterModel : DataView<Character> {
        [SerializeField] private ResourceStatView healthView;
        [SerializeField] private GameObject turnMarker;
        [SerializeField] private GameObject targetMark;
        [SerializeField] private GameObject focusCamera;
        private Animator _animator;

        public GameObject Camera => focusCamera;
        public GameObject TargetMark => targetMark;

        public void SelectAsTarget() => Data.cm.View.SelectTarget(Data);

        protected override void Subscribe() {
            //healthView.SetData(Data.Health);
            _animator = Instantiate(Data.member.Prefab, transform);
            _animator.Play("Spawn");

            Data.PlayAnimation += _animator.Play;
            Data.OnResolveDefend += DamagePopup;
            Data.StatusEffectList.OnStatusAdded += StatusMessage;
            Data.OnStartTurn += ActivateTurnMarker;
            Data.OnEndTurn += DeactivateTurnMarker;
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
            var view = Data.cm.View;
            var color = status.Source.TextColor;
            var dName = status.Source.DisplayName;
            StartCoroutine(view.callPopup.Pop(dName, color, transform.position, 0.5f));
        }

        private void DamagePopup(CombatArgs args) {
            _animator.Play("Hit", 0);

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
            var view = Data.cm.View;

            if(!args.result.Miss && args.result.ResistStatus) {
                if(popupValue == 0)
                    popupText = "Resist";
                else
                    popupText += "\nResist";
            }

            StartCoroutine(view.callPopup.Pop(popupText, damageColor, transform.position, 0));
            if(args.result.IsFatal) _animator.Play("Die", 0, 0);
            if(args.result.IsRevive) _animator.Play("Spawn", 0, 0);
        }
    }
}