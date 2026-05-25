namespace TurnBasedRPG.Data {
    public interface IConsumable : IItem {
        ISkill Skill { get; }
    }
}