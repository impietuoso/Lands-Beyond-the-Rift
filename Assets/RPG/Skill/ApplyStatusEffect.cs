using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.SkillEffects;
using TurnBasedRPG.StatusEffect;
using UnityEngine;

[Serializable, Obsolete]
public class ApplyStatusEffect : ISkillEffect {
    public StatusSO status;
    public bool targetUser;
    public void Prepare(CombatArgs args) {
        if(args.hitChance == 0) args.hitChance = 100;
        if (targetUser) {
            args.user?.StatusEffectList.Apply(status);
            Debug.Log(status.status.statusName + " was apply on " + args.user?.characterName + ".");
        } else args.statusEffects.Add(status);
    }

    public ICombatEffect GetUpgrade(Skill skill) {
        return new ApplyEffect
        {
            status = status,
            targetUser = targetUser,
        };
    }
}
