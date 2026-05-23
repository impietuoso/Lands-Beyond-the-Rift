using TurnBasedRPG.BattleStats;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills;
using UnityEngine;

namespace TurnBasedRPG.Data
{
    [CreateAssetMenu(menuName = "Scriptable/Item/Equipment", fileName = "New Equipment")]
    public class Equipment : Item, IStats {
        public EquipmentType _equipmentType;
        public Tag _category;
        public StatsBase _bonusValue;
        public Skill _equipmentSkill;
        [SerializeReference, TypeDropdown(typeof(IPassive))] public IPassive _passiva;

        public EquipmentType Type;
        public Tag Category;
        public Skill equipmentSkill;
        [SerializeReference, TypeDropdown(typeof(IPassive))] public IPassive passiva;
        
        public string BonusText() {
            var resume = "<b>" + displayName + ":</b>\n";
            // foreach (var attr in Attributes.All) {
            //     var v = this[attr];
            //     if (v > 0) resume += $"{attr}: {v}\n";
            // }
            foreach (var stat in Stats.All) {
                var v = this[stat];
                if (v > 0) resume += $"{stat}: {v}\n";
            }
            return resume;
        }

        public virtual int this[Stat s] => 0;
    }
}