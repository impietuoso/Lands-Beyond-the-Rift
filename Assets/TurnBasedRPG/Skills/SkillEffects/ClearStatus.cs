using System;
using System.Linq;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.StatusEffects;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class ClearStatus : ISkillEffect, ISingleLineDrawer {
        public StatusType type;

        public void Prepare(CombatArgs args) {
            var list = args.target.StatusEffectList;
            var toRemove = list.StatusList.Keys.Where(k => k.Type == type);
            foreach (var status in toRemove)
                args.target.StatusEffectList.Remove(status);
        }
    }
}