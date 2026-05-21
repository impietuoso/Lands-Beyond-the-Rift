using System;
using TurnBasedRPG.Data;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class RemoveStatus : ISkillEffect {
        public StatusSO status;

        public void Prepare(CombatArgs args) {
            args.target.StatusEffectList.Remove(status);
        }
    }
}