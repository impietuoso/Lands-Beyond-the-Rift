using System;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.SkillEffects;
using UnityEngine;


[Serializable, Obsolete]
public class DefenseSkill : ISkillEffectOld {
    [Range(0f, 1f)]
    public float damageReduction;

    public void Prepare(CombatArgs args) {
        args.user.OnStartTurn += OnStartTurn;
        args.user.OnDefend += OnDefend;
        args.unavoidable = true;
    }

    private void OnDefend(CombatArgs args) {
        args.damage = (int)(args.damage * (1f - damageReduction));
    }
    
    private void OnStartTurn(Character target) {
        target.OnStartTurn -= OnStartTurn;
        target.OnDefend -= OnDefend;
    }

    public ISkillEffectOld GetUpgrade(ISkill skill) => new Defense
    {
        damageReduction = damageReduction,
    };
}