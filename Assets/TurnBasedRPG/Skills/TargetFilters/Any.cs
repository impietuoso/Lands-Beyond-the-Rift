namespace TurnBasedRPG.Skills.TargetFilters {
    public class Any : ITargetFilter {
        public bool Match(Character _, Character __) => true;
    }
}