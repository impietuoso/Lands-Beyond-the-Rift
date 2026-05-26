using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class ConditionalEffect : ISkillEffect {
        [SerializeReference, TypeInstance] public ICombatArgsFilter condition;
        [SerializeReference, TypeInstance] public ISkillEffectOld[] effects;

        public void Prepare(CombatArgs args) {
            if(!condition.Match(args)) return;
            foreach (var effect in effects)
                effect.Prepare(args);
        }
    }
}