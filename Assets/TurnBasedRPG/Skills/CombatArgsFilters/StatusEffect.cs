using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.CombatArgsFilters {
    [Preserve, Serializable]
    public class StatusEffect : ICombatArgsFilter, ISingleLineDrawer {
        public Data.StatusSO status;
        public bool not;
        public bool Match(CombatArgs args) => args.target.StatusEffectList.Contain(status) ^ not;
    }
}