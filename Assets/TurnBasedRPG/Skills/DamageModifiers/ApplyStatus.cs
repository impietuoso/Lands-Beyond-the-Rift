using System;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.DamageModifiers {
    [Preserve, Serializable]
    public class ApplyStatus : IDamageModifier, ISingleLineDrawer {
        [Label("")] public StatusSO status;
        [Label("Usr")] public bool targetUser;

        public void ModifyArgs(CombatArgs args) => args.OnResolve += ApplyIfDamaged;

        private void ApplyIfDamaged(CombatArgs args) {
            if(args.result.Miss) return;
            var target = targetUser ? args.user : args.target;
            args.TryApplyStatuses(target, status);
        }
    }
}