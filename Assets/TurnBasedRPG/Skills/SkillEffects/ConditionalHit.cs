using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class ConditionalHit : ISkillEffect {
        [SerializeReference, TypeInstance] public ICombatArgsFilter condition;
        [SerializeReference, TypeInstance] public ISkillEffect[] effects;

        public void Prepare(CombatArgs args) {
            args.OnResolve += TryTrigger;
        }

        private void TryTrigger(CombatArgs args) {
            if(!condition.Match(args)) return;
            var chain = args.Chain();
            foreach (var effect in effects)
                effect.Prepare(chain);
            chain.Resolve();
        }
    }
}