namespace TurnBasedRPG.Data {
    public interface IWeapon : IEquipment {
        public ISkill Attack { get; }
    }
}