using System;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class Heal : ICombatEffect {
        public int amount;
        public AttributeScale scale;

        public void Prepare(CombatArgs args) {
            args.heal = amount + scale.GetValue(args.user);
            args.unavoidable = true;
            args.cannotCrit = true;
        }
    }
}