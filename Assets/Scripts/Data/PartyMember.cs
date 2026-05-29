using System;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills;
using UnityEngine;
using Attribute = TurnBasedRPG.BattleStats.Attribute;

namespace Data {
    [Serializable, CreateAssetMenu(menuName = "Scriptable/PartyMember")]
    public class PartyMember : ScriptableObject, IPartyMember {
        [SerializeField] private string charName;
        [SerializeField] private int level;
        [SerializeField] private int exp;
        [SerializeField] private int hp;
        [SerializeField] private int mp;
        [SerializeField] private Attributes usedAttributes;
        [SerializeField] private Profession profession;
        [SerializeField] private Element element;
        [SerializeField, ShowEquipmentTypeAtribute] private ObservableList<Equipment> equips;
        [SerializeField, Fixed(3)] private ObservableList<Consumable> consumables;
        [SerializeField] private ObservableList<Skill> equipedSkills;
        [SerializeField] private ObservableList<Skill> learnedSkills;
        [SerializeField] private Sprite characterSprite;
        [SerializeField] private Sprite uiSprite;
        [SerializeField] private Animator prefab;

        public string CharName => charName;
        public int Level { get => level; set => level = value; }
        public int CurrentHp { get => hp; set => hp = value; }
        public int CurrentMp { get => mp; set => mp = value; }
        public int Exp { get => exp; set => exp = value; }
        public Attributes UsedAttributes => usedAttributes;
        public Profession Profession => profession;
        public Element Element => element;
        public IReadOnlyList<IEquipment> Equips => equips;
        public IReadOnlyList<Consumable> Consumables => consumables;
        public IReadOnlyList<ISkill> EquipedSkills => equipedSkills;
        public IReadOnlyList<ISkill> LearnedSkills => learnedSkills;
        public Sprite CharacterSprite => characterSprite;
        public Sprite UISprite => uiSprite;
        public Animator Prefab => prefab;
        public ISkill BasicAttack => equips[0] is Weapon w ? w.Attack : profession.BasicAttack;

        public IEnumerable<IPassive> GetPassives() {
            foreach (var e in equips)
            foreach (var p in e.GetPassives())
                yield return p;
        }

        public IEnumerable<ISkill> GetSkills() {
            foreach (var s in equipedSkills)
                if(s)
                    yield return s;

            foreach (var e in equips)
                if(e.ActiveSkill != null)
                    yield return e.ActiveSkill;
        }

        public int GetUnusedPoints() => level * 2 - usedAttributes.Sum();
        public void SetEquip(int index, IEquipment e) => equips[index] = (Equipment)e;
        public void SetSkill(int index, ISkill e) => equipedSkills[index] = (Skill)e;
        public void LearnSkill(ISkill e) => learnedSkills.Add((Skill)e);

        public int this[Attribute a] => profession[a] + usedAttributes[a];
        public int this[Stat s] => equips.Sum(i => i != null ? i[s] : 0);
    }
}