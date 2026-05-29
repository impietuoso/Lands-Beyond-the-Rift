using System.Collections.Generic;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills;
using UnityEngine;

namespace Data {
    [CreateAssetMenu(menuName = "Scriptable/Item/Equipment", fileName = "New Equipment")]
    public class Equipment : Item, IEquipment {
        [Separator]
        [SerializeField] private EquipmentType type;
        [SerializeField] private Tag category;
        [SerializeField] private StatsBase bonusValue;
        [SerializeField] private Skill skill;
        [SerializeField] private Passive tacticsBuff;
        [SerializeReference, TypeDropdown(typeof(IPassive))] private IPassive passive;

        public EquipmentType Type => type;
        public Tag Category => category;
        public ISkill ActiveSkill => skill;
        public IPassive Passive => passive;

        public string BonusText() {
            var resume = "<b>" + displayName + ":</b>\n";
            foreach (var stat in Stats.All) {
                var v = this[stat];
                if(v > 0) resume += $"{stat}: {v}\n";
            }

            return resume;
        }

        public IEnumerable<IPassive> GetPassives() {
            if(tacticsBuff) yield return tacticsBuff;
            if(passive != null) yield return passive;
        }

        public virtual int this[Stat s] => bonusValue[s];
    }
}