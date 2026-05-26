using System.Collections.Generic;
using TurnBasedRPG.Data;

namespace TurnBasedRPG.Skills {
    public class SkillArgs {
        public ISkill Skill;
        public Character User;
        public Character Target;

        public SkillArgs() { }

        public SkillArgs(ISkill skill, Character user, Character target) {
            Skill = skill;
            User = user;
            Target = target;
        }

        public CombatManager Cm => Target.cm;
        public IEnumerable<Character> Targets => Skill.Targeting.GetAffectedTargets(User, Target);
    }
}