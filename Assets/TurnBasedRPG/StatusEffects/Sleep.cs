using System;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.StatusEffects {
    [Preserve, Serializable]
    public class Sleep : StatusBase {
        
        public override void Apply(Character target) {
            base.Apply(target);
            target.Stun.Add(Source);
            target.OnResolveDefend += OnDefend;
        }

        public override void Remove(Character target) {
            base.Remove(target);
            target.Stun.Remove(Source);
            target.OnResolveDefend -= OnDefend;
        }
        
        private void OnDefend(CombatArgs args) {
            if(args.result.TotalDamage <= 0) return;
            Debug.Log(args.target.characterName + " woke up from damage!");
            args.target.StatusEffectList.Remove(Source);
        }
    }
}