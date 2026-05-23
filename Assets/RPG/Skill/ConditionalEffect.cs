using System;
using System.Collections.Generic;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills;
using UnityEngine;

[Serializable, Obsolete]
public class ConditionalEffect : ISkillEffectOld {
    public StatusSO status;
    [SerializeReference, TypeDropdown(typeof(ISkillEffectOld))]
    public List<ISkillEffectOld> effect;

    public void Prepare(CombatArgs args) {
        if (args.target.StatusEffectList.Contain(status)) {
            foreach (var item in effect) {
                item.Prepare(args);
            }
        }
    }
}