using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class Shield : ICombatEffect, ISingleLineDrawer {
        public int value;
        [Label(null)] public AttributeScale scale;

        public void Prepare(CombatArgs args) {
            args.shield += value + scale.GetValue(args.user);
        }
    }
}