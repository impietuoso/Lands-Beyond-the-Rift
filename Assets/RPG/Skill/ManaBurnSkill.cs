using System;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.SkillEffects;
using UnityEngine;

[Serializable, Obsolete]
public class ManaBurnSkill : ISkillEffectOld {
    public bool useDamageDealt;
    public int manaBurn;
    [Range(0f, 1f)]
    public float damagePercentage;

    public void Prepare(CombatArgs args) {
        if(useDamageDealt) args.OnResolve += GetDamage;
        else args.mana = -manaBurn;
    }

    public void GetDamage(CombatArgs args) {
        var damage = args.result.deltaHp * damagePercentage;
        args.mana = -(int)damage;
    }

    public ISkillEffectOld GetUpgrade(Skill skill) {
        if(useDamageDealt) {
            return new ManaBurn
            {
                damagePercent = damagePercentage,
            };
        }

        return new Mana
        {
            amount = manaBurn,
        };
    }
}