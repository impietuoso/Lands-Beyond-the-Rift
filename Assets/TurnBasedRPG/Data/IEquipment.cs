using TurnBasedRPG.BattleStats;
using TurnBasedRPG.Skills;

namespace TurnBasedRPG.Data {
    public interface IEquipment : IItem, IStats {
        EquipmentType Type { get; }
        Tag Category { get; }
        ISkill ActiveSkill { get; }
        IPassive Passive { get; }
        string BonusText();
    }
}