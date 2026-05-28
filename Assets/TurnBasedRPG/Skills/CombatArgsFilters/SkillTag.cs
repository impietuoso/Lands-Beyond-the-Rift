using System;
using System.Linq;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.CombatArgsFilters {
    [Preserve, Serializable]
    public class SkillTag : ICombatArgsFilter, ISingleLineDrawer {
        public Tag tag;
        public bool not;
        public bool Match(CombatArgs args) => args.skill?.Tags.Contains(tag) ^ not ?? false;
    }
}