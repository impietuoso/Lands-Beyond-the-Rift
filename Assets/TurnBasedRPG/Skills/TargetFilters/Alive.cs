namespace TurnBasedRPG.Skills.TargetFilters {
    public class Alive : ITargetFilter {
        public bool Match(Character user, Character tgt) => !tgt.Dead;
    }
}