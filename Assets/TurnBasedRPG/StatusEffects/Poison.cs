using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.StatusEffects {
    [Preserve, Serializable]
    public class Poison : Damage {
        [SerializeField] private float healMult = 0.6f;

        public override void Apply(Character target) {
            base.Apply(target);
            target.OnDefend += OnDefend;
        }

        public override void Remove(Character target) {
            base.Remove(target);
            target.OnDefend -= OnDefend;
        }

        private void OnDefend(CombatArgs args) {
            if(args.heal <= 0) return;
            args.heal = (int)(args.heal * healMult);
        }
    }
}