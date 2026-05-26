using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.DamageModifiers {
    [Preserve, Serializable]
    public class DamageMod : IDamageModifier, ISingleLineDrawer {
        public enum Mode { Add, Mult }

        public Mode mode;
        [Label(null)] public float value;

        public void ModifyArgs(CombatArgs args) {
            if(mode == Mode.Add) args.damage += (int)value;
            else args.damage = (int)(args.damage * value);
        }
    }
}