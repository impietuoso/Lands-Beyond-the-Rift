using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class PercentDamage : ISkillEffect {
        [Range(0f, 1f)] public float percent = 0.25f;
        [Range(0, 100)] public int hitChance = 100;

        public void Prepare(CombatArgs args) {
            var damage = Mathf.RoundToInt(args.target.Health.Max * percent);
            args.damage = damage;
            args.hitChance = hitChance;
            args.ignoreArmor = true;
            args.cannotCrit = true;
        }
    }
}