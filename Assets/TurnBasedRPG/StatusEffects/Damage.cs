using System;
using TurnBasedRPG.Data;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.StatusEffects {
    [Preserve, Serializable]
    public class Damage : StatusBase {
        public enum Trigger {
            TurnStart,
            TurnEnd
        }

        [SerializeField] private Trigger trigger;
        [SerializeField] private int damage = 10;
        [SerializeField] private Element element;
        [SerializeField] private bool ignoreArmor = true;
        [SerializeField] private bool ignoreShield = true;

        protected override void StartTurn(Character target) {
            base.StartTurn(target);
            if(trigger == Trigger.TurnStart) OnTrigger(target, damage);
        }

        protected override void EndTurn(Character target) {
            base.EndTurn(target);
            if(trigger == Trigger.TurnEnd) OnTrigger(target, damage);
        }

        protected void OnTrigger(Character target, int dmg) {
            var args = new CombatArgs
            {
                source = this,
                element = element,
                target = target,
                damage = damage,
                ignoreShield = ignoreShield,
                ignoreArmor = ignoreArmor,
                stopReactionAttacks = true,
                unavoidable = true,
            };
            args.Resolve();
            Debug.Log($"{target.member.CharName} takes {damage} {Source.DisplayName} damage.");
        }
    }
}