using System;
using TurnBasedRPG.Data;
using TurnBasedRPG.StatusEffect;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class StatusRemoval : ICombatEffect {
        public StatusSO[] statuses;

        public void Prepare(CombatArgs args) {
            foreach (var status in statuses)
                args.target.StatusEffectList.Remove(status);
        }
    }
}