using System;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable, Obsolete]
    public class ApplyEffect : ISkillEffect, ISingleLineDrawer {
        public StatusSO status;
        [Label("tgtUsr")] public bool targetUser;

        public void Prepare(CombatArgs args) {
            args.OnResolve += TryApply;
        }

        private void TryApply(CombatArgs args) {
            var target = targetUser ? args.user : args.target;
            args.TryApplyStatuses(target, status);
        }
    }
}