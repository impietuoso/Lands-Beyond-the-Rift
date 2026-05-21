using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class OnHitEffects : ISkillEffect, ISingleLineDrawer {
        [SerializeReference, TypeInstance] public IOnHitEffect[] effects;

        public void Prepare(CombatArgs args) {
            args.OnResolve += ChainOnHit;
        }

        private void ChainOnHit(CombatArgs args) {
            if(args.result.Miss) return;
            foreach (var effect in effects)
                effect.OnHit(args);
        }
    }
}