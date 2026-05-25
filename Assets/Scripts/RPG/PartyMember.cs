using System;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using Attribute = TurnBasedRPG.BattleStats.Attribute;

namespace RPG {
    [Serializable, CreateAssetMenu(fileName = "New Party Member", menuName = "Scriptable/PartyMember")]
    public class PartyMember : ScriptableObject, IPartyMember {
        [SerializeField] private string charName;
        [SerializeField] private int level;
        [SerializeField] private Attributes usedAttributes;
        [SerializeField] private Profession profession;
        [SerializeField] private Element element;
        [SerializeField, ShowEquipmentTypeAtribute] private ObservableList<Equipment> equips;
        [SerializeField] private ObservableList<Skill> equipedSkills;
        [SerializeField] private ObservableList<Skill> learnedSkills;
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Sprite uiSprite;
        [SerializeField] private Animator prefab;

        public string CharName => charName;
        public int Level { get => level; set => level = value; }
        public Attributes UsedAttributes => usedAttributes;
        public Profession Profession => profession;
        public Element Element => element;
        public IReadOnlyList<IEquipment> Equips => equips;
        public IReadOnlyList<ISkill> EquipedSkills => equipedSkills;
        public IReadOnlyList<ISkill> LearnedSkills => learnedSkills;
        public Sprite CharacterSprite => characterSprite;
        public Sprite UISprite => uiSprite;
        public Animator Prefab => prefab;

        public int GetUnusedPoints() => level * 2 - usedAttributes.Sum();
        public void SetEquip(int index, IEquipment e) => equips[index] = (Equipment)e;
        public void SetSkill(int index, ISkill e) => equipedSkills[index] = (Skill)e;
        public void LearnSkill(ISkill e) => learnedSkills.Add((Skill)e);

        public int this[Attribute a] => profession[a] + usedAttributes[a];
        public int this[Stat s] => equips.Sum(i => i != null ? i[s] : 0);
    }
}