using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class Mana : ISkillEffect, ISingleLineDrawer {
        public int amount;

        public void Prepare(CombatArgs args) {
            args.mana += amount;
        }
    }
}