namespace TurnBasedRPG.Skills {
    public class SkillArgs {
        public Data.Skill Skill;
        public Character User;
        public Character Target;
        public CombatManager Cm => Target.cm;
    }
}