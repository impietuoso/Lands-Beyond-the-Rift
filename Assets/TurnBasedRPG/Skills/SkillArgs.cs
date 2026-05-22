using TurnBasedRPG.Data;

namespace TurnBasedRPG.Skills {
    public class SkillArgs {
        public Skill Skill;
        public Character User;
        public Character Target;

        public SkillArgs() { }

        public SkillArgs(Skill skill, Character user, Character target) {
            Skill = skill;
            User = user;
            Target = target;
        }

        public CombatManager Cm => Target.cm;
    }
}