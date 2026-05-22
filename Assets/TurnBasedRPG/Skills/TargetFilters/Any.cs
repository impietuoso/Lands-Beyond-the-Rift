namespace TurnBasedRPG.Skills.TargetFilters {
    public class Any : ITargetFilter {
        public bool Match(Character user, Character tgt) => true;
    }
}