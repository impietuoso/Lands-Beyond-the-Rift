using System.Collections.Generic;
using TurnBasedRPG.Data;

namespace TurnBasedRPG.Skills {
    public interface ISkillEffect {
        void Prepare(CombatArgs args);
        ICombatEffect GetUpgrade(Skill skill) => null;
    }

    public interface ICombatEffect : ISkillEffect {
        ICombatEffect ISkillEffect.GetUpgrade(Skill skill) => null;
    }
}