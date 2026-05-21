using System;
using System.Linq;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.StatusEffect;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class StatusRemoveAll : ICombatEffect, ISingleLineDrawer {
        public StatusType type;

        public void Prepare(CombatArgs args) {
            var list = args.target.StatusEffectList;
            var toRemove = list.StatusList.Keys.Where(k => k.statusType == type);
            foreach (var status in toRemove)
                args.target.StatusEffectList.Remove(status);
        }
    }
}