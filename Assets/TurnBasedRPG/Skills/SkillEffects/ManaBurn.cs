using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class ManaBurn : ISkillEffect, ISingleLineDrawer {
        [Label("Dmg%"), Range(0, 1)] public float damagePercent;

        public void Prepare(CombatArgs args) {
            args.OnResolve += Burn;
        }

        public void Burn(CombatArgs args) {
            var value = args.result.TotalDamage * damagePercent;
            var chain = args.Chain();
            args.mana = Math.Max(0, -(int)value); 
            chain.Resolve();
        }
    }
}