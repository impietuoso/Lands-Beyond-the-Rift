using System;
using System.Linq;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using Attribute = TurnBasedRPG.BattleStats.Attribute;

namespace TurnBasedRPG.Data
{
    [Serializable, CreateAssetMenu(fileName = "New Party Member", menuName = "Scriptable/PartyMember")]
    public class PartyMember : ScriptableObject, IAttributes, IStats {
        public string charName;
        public int level;
        public Attributes usedAttributes;
        public Profession profession;
        public Element element; 
        [ShowEquipmentTypeAtribute] public ObservableList<Equipment> equips;
        public ObservableList<Skill> equipedSkills;
        public ObservableList<Skill> learnedSkills;
        public Sprite characterSprite;
        public Sprite uiSprite;

        public int GetUnusedPoints() => level * 2 - usedAttributes.Sum();
        
        public int this[Attribute a] => profession[a] + usedAttributes[a];
        public int this[Stat s] => equips.Sum(i => i[s]);
    }
}