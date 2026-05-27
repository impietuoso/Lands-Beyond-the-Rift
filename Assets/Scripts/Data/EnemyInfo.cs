using System;
using System.Collections.Generic;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using Attribute = TurnBasedRPG.BattleStats.Attribute;

namespace RPG {
    [Serializable, CreateAssetMenu(menuName = "Scriptable/EnemyInfo")]
    public class EnemyInfo : ScriptableObject, IPartyMember {
        [SerializeField] private string charName;
        [SerializeField] private int level;
        [SerializeField] private int exp;
        [SerializeField] private Element element;
        [SerializeField] private Sprite portrait;
        [SerializeField] private Animator prefab;
        [Separator]
        [SerializeField] private Attributes attributes;
        [SerializeField] private StatsBase statsBonus;
        [SerializeField] private Skill[] skills;
        [SerializeField] private Drop[] drops;

        [Serializable]
        public class Drop : ISingleLineDrawer {
            [field: SerializeField] public Item Item { get; private set; }
            [field: SerializeField, Range(0, 1), Label("%")] public float Chance { get; private set; }
        }

        public string CharName => charName;
        public int Level { get => level; set => level = value; }
        public int Exp { get => exp; set => exp = value; }
        public Attributes UsedAttributes => attributes;
        public Profession Profession => null;
        public Element Element => element;
        public IReadOnlyList<IEquipment> Equips => Array.Empty<IEquipment>();
        public IReadOnlyList<ISkill> EquipedSkills => skills;
        public IReadOnlyList<ISkill> LearnedSkills => skills;
        public Sprite CharacterSprite => portrait;
        public Sprite UISprite => portrait;
        public Animator Prefab => prefab;
        public IReadOnlyList<Drop> Drops => drops;

        public int this[Attribute a] => attributes[a] + 2;
        public int this[Stat s] => statsBonus[s];

        public int GetUnusedPoints() => level * 2 - attributes.Sum();
        public void SetEquip(int index, IEquipment e) { }
        public void SetSkill(int index, ISkill e) => skills[index] = (Skill)e;
        public void LearnSkill(ISkill e) => throw new InvalidOperationException();
    }
}