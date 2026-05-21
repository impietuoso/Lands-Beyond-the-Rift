using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.CombatArgsFilters {
    [Preserve, Serializable]
    public class Element : ICombatArgsFilter, ISingleLineDrawer {
        public Data.Element element;
        public bool Match(CombatArgs args) => args.element == element;
    }
}