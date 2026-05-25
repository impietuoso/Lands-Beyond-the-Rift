using System;
using System.Collections.Generic;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.SkillEffects;
using TurnBasedRPG.StatusEffects;

[Serializable, Obsolete]
public class StatusRemoval : ISkillEffectOld {
    public StatusType statusType;
    public bool removeAll;
    public StatusSO removedStatus;

    public void Prepare(CombatArgs args) {
        var statusList = args.target.StatusEffectList.StatusList;
        if(removeAll) {
            var toRemove = new List<StatusSO>();

            foreach (var kvp in statusList) {
                if(kvp.Key.Type == statusType) {
                    toRemove.Add(kvp.Key);
                }
            }

            foreach (var so in toRemove) {
                args.target.StatusEffectList.Remove(so);
            }
        }
        else {
            StatusSO so = null;
            foreach (var kvp in statusList) {
                if(kvp.Key.Status == removedStatus.Status) {
                    so = kvp.Key;
                }
            }

            if(so.Status != null) args.target.StatusEffectList.Remove(so);
        }
    }

    public ISkillEffectOld GetUpgrade(ISkill skill) {
        if(removeAll) {
            return new ClearStatus
            {
                type = statusType,
            };
        }

        return new RemoveStatus
        {
            status = removedStatus
        };
    }
}