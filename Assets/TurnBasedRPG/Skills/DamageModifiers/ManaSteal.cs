using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.DamageModifiers {
    [Preserve, Serializable]
    public class ManaSteal : IDamageModifier, ISingleLineDrawer {
        [Range(0f, 1f)] public float percent = 0.2f;

        public void ModifyArgs(CombatArgs args) {
            args.OnResolve += Steal;
        }

        private void Steal(CombatArgs args) {
            var heal = args.result.Mana.Delta * percent;
            if (heal >= 0) return;

            var chain = args.Chain(this, args.user);
            chain.mana = (int)-heal;
            chain.Resolve();
        }
    }
}