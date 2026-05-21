using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class HealPercent : ISkillEffect, ISingleLineDrawer {
        [Range(0f, 1f)] public float percent = 0.2f;

        public void Prepare(CombatArgs args) {
            args.heal = (int)(args.target.Health.Max * percent);
            args.unavoidable = true;
            args.cannotCrit = true;
        }
    }
}