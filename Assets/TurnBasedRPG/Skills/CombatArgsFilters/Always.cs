using System;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.CombatArgsFilters {
    [Preserve, Serializable]
    public class Always : ICombatArgsFilter {
        public bool Match(CombatArgs args) => true;
    }

    [Preserve, Serializable]
    public class None : ICombatArgsFilter {
        public bool Match(CombatArgs args) => true;
    }
}