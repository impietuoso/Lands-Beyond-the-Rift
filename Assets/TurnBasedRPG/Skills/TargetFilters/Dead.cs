namespace TurnBasedRPG.Skills.TargetFilters {
    public class Dead : ITargetFilter {
        public bool Match(Character user, Character tgt) => tgt.Dead;
    }
}