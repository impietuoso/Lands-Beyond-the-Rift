using System;
using TurnBasedRPG;
using TurnBasedRPG.Skills;
using UnityEngine;

[Serializable, Obsolete]
public class ManaDrainSkill : ISkillEffectOld {
    //valor de mana é multiplicado por variavel fixa ou dano causado
    [Range(0f, 1f)] public float damagePercentage = 0.2f;

    public void Prepare(CombatArgs args) {
        args.OnResolve += Steal;
    }

    public void Steal(CombatArgs args) {
        args.OnResolve -= Steal;
        var stealMana = args.result.deltaMp * damagePercentage;
        if (stealMana == 0) return;
        var chain = args.Chain(this, args.user);
        chain.mana = -(int)stealMana;
        chain.Resolve();
    }
}