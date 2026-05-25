using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.SkillEffects;
using UnityEngine;
using Attribute = TurnBasedRPG.BattleStats.Attribute;

[Serializable, Obsolete]
public class HealSkill : ISkillEffectOld {
    public int healAmount;
    public bool isPercentageHeal;
    [Range(0f, 1f)]
    public float healthPercentage = 0.2f;
    public float statMultiplier;
    public Attribute healStatScale;

    public void Prepare(CombatArgs args) {
        int finalHeal = 0;
        if(isPercentageHeal) {
            finalHeal = Mathf.RoundToInt(args.target.Health.Max * healthPercentage);
        }
        else {
            var healStat = healStatScale;
            var healStatValue = args.user[(Attribute)(int)healStat] * statMultiplier;

            finalHeal = Mathf.RoundToInt(healAmount + healStatValue);
        }

        args.heal = finalHeal;
        args.critChance = 0;
        args.hitChance = 100;
    }

    public ISkillEffectOld GetUpgrade(ISkill skill) {
        if(isPercentageHeal) {
            return new HealPercent
            {
                percent = healthPercentage
            };
        }

        return new Heal
        {
            amount = healAmount,
            scale = new AttributeScale
            {
                attribute = healStatScale,
                scale = statMultiplier
            }
        };
    }
}