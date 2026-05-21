namespace TurnBasedRPG.Skills {
    public interface ITargetFilter {
        bool Match(Character user, Character target);
    }
}