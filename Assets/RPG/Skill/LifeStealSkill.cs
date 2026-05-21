using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using UnityEngine;

[Serializable]
public class LifeStealSkill : ISkillEffect {
    [Range(0f, 1f)]
    public float damagePercentage = 0.2f;

    public void Prepare(CombatArgs args) {
        args.OnResolve += Steal;
    }

    public void Steal(CombatArgs args) {
        args.OnResolve -= Steal;
        var stealHeal = args.result.deltaHp * damagePercentage;
        if (stealHeal == 0) return;
        var chain = args.Chain(this, args.user);
        chain.heal = (int)stealHeal;
        chain.Resolve();
    }
}