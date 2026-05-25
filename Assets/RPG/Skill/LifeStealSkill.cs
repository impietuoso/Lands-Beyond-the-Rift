using System;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.DamageModifiers;
using UnityEngine;

[Serializable, Obsolete]
public class LifeStealSkill : ISkillEffectOld {
    [Range(0f, 1f)]
    public float damagePercentage = 0.2f;

    public void Prepare(CombatArgs args) {
        args.OnResolve += Steal;
    }

    public void Steal(CombatArgs args) {
        var stealHeal = args.result.deltaHp * damagePercentage;
        if (stealHeal == 0) return;
        var chain = args.Chain(this, args.user);
        chain.heal = (int)stealHeal;
        chain.Resolve();
    }

    public ISkillEffectOld GetUpgrade(ISkill skill) {
        return new LifeSteal
        {
            percent = damagePercentage,
        };
    }
}