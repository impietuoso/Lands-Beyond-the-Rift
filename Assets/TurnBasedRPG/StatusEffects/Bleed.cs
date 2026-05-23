using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.StatusEffects {
    [Preserve, Serializable]
    public class Bleed : Damage {
        [SerializeField] private int extraDamage = 12;

        public override void Apply(Character target) {
            base.Apply(target);
            target.OnAttack += OnAttack;
        }

        public override void Remove(Character target) {
            base.Remove(target);
            target.OnAttack -= OnAttack;
        }

        private void OnAttack(CombatArgs args) {
            if(args.damage <= 0) return;
            OnTrigger(args.user, extraDamage);
        }
    }
}