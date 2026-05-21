using System;
using System.Collections.Generic;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.SkillEffects;
using UnityEngine;
using Attribute = TurnBasedRPG.BattleStats.Attribute;

[Obsolete, Serializable]
public class DamageSkill : ISkillEffect {
    public int baseDamage;
    public bool ignoreShield;
    public bool isPercentageDamage;
    [Range(0f, 1f)] public float healthPercentage = 0.2f;
    [Range(0, 100)] public int hitChance;
    [Range(0, 100)] public int criticalChance;
    public float statMultiplier;
    public Attribute damageStatScale; //TODO change to new
    public float damageRange = 0.15f;

    public void Prepare(CombatArgs args) {
        int finalDamage;
        if(isPercentageDamage) {
            finalDamage = Mathf.RoundToInt(args.target.Health.Max * healthPercentage);
        }
        else {
            float rangedDamage = UnityEngine.Random.Range(1 - damageRange, 1 + damageRange);
            var damageStat = damageStatScale;
            var damageStatValue = args.user[(Attribute)(int)damageStat] * statMultiplier;
            finalDamage = Mathf.RoundToInt((baseDamage + damageStatValue) * rangedDamage);
        }

        args.ignoreShield = ignoreShield;
        args.damage = finalDamage;
        args.critChance = criticalChance;
        args.hitChance = hitChance;
    }

    public ICombatEffect GetUpgrade(Skill skill) {
        // var statuses = new List<IDamageModifier>();
        //
        // for (var i = 0; i < effects.Count; i++) {
        //     if(effects[i] is not ApplyStatusEffect a) continue;
        //     statuses.Add(new ApplyStatus { status = a.status, targetUser = a.targetUser });
        //     effects[i] = null;
        // }

        if(isPercentageDamage) {
            return new PercentDamage
            {
                percent = healthPercentage,
                hitChance = hitChance,
                //modifiers = statuses.ToArray(),
            };
        }

        return new Damage
        {
            damage = baseDamage,
            scale = new AttributeScale
            {
                scale = statMultiplier,
                attribute = damageStatScale
            },
            hitChance = hitChance,
            critChance = criticalChance,
            damageRange = damageRange,
            //modifiers = statuses.ToArray(),
        };
    }
}