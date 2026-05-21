using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class Shield : ISkillEffect, ISingleLineDrawer {
        public int value;
        [Label(null)] public AttributeScale scale;

        public void Prepare(CombatArgs args) {
            args.shield += value + scale.GetValue(args.user);
        }
    }

    [Preserve, Serializable]
    public class PercentShield : ISkillEffect, ISingleLineDrawer {
        [Range(0, 1)] public float percent;

        public void Prepare(CombatArgs args) {
            args.shield += (int)(args.target.Health.Max * percent);
        }
    }
}