using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG;
using TurnBasedRPG.Skill;
using UnityEngine;

[Serializable]
public class HealSkill : ISkillEffect {
    public int healAmount;
    public bool isPercentageHeal;
    [Range(0f, 1f)]
    public float healthPercentage = 0.2f;
    public float statMultiplier;
    public StatName healStatScale;
    
    public void Prepare(CombatArgs args) {
        int finalHeal = 0;
        if (isPercentageHeal) {
            finalHeal = Mathf.RoundToInt(args.target.Health.Max * healthPercentage);
        } else {
            var healStat = healStatScale;
            var healStatValue = args.user[(Attribute)(int)healStat] * statMultiplier;

            finalHeal = Mathf.RoundToInt(healAmount + healStatValue);
        }

        args.heal = finalHeal;
        args.critChance = 0;
        args.hitChance = 100;
    }
}