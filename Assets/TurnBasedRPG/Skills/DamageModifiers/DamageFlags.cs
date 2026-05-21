using System;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.DamageModifiers {
    [Preserve, Serializable]
    public class IgnoreShield : IDamageModifier {
        public void ModifyArgs(CombatArgs args) => args.ignoreShield = true;
    }

    [Preserve, Serializable]
    public class IgnoreArmor : IDamageModifier {
        public void ModifyArgs(CombatArgs args) => args.ignoreArmor = true;
    }

    [Preserve, Serializable]
    public class Unavoidable : IDamageModifier {
        public void ModifyArgs(CombatArgs args) => args.unavoidable = true;
    }

    [Preserve, Serializable]
    public class StopReactions : IDamageModifier {
        public void ModifyArgs(CombatArgs args) => args.stopReactionAttacks = true;
    }

    [Preserve, Serializable]
    public class CannotCrit : IDamageModifier {
        public void ModifyArgs(CombatArgs args) => args.cannotCrit = true;
    }
}