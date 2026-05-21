namespace TurnBasedRPG.Skills {
    public interface ICombatArgsFilter {
        bool Match(CombatArgs args);
    }
}