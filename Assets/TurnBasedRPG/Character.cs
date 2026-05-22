using System;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.Data;
using TurnBasedRPG.StatusEffect;
using UnityEngine;

namespace TurnBasedRPG {
    [Serializable]
    public partial class Character {
        public Character(CombatManager cm, PartyMember member, string teamName,
            List<Character> allies, List<Character> enemies) {
            this.cm = cm;
            this.member = member;
            team = teamName;
            Allies = allies;
            Enemies = enemies;
            Element = member.element;

            GetSkills();
            InitializeStats();
            OnSetup?.Invoke(this);
        }

        public CombatManager cm { get; }
        public IReadOnlyList<Character> Allies { get; }
        public IReadOnlyList<Character> Enemies { get; }

        [Header("Advancement Info")]
        public PartyMember member;
        public string team;
        public Element Element;
        public Skill basicAttack;
        public List<Skill> skills = new();
        public StatusEffectList StatusEffectList;

        public IObservableList<Equipment> equipment => member.equips;
        public string characterName => member.charName;
        public Profession profession => member.profession;
        public bool Dead => Health.Current == 0;

        public Action<Character> OnSetup;
        public Action<Character> OnStartTurn;
        public Action<Character> OnEndTurn;
        public Action<CombatArgs> OnDefend;
        public Action<CombatArgs> OnAttack;
        public Action<CombatArgs> OnResolveDefend;
        public Action<CombatArgs> OnResolveAttack;
        public Action<string, int> PlayAnimation;

        public Observable<float> actionPoints = new();

        public void SubscribePassives() {
            foreach (var e in member.equips.Where(i => i))
                e.passiva?.Subscribe(this);
        }

        public void UnsubscribePassives() {
            foreach (var e in member.equips.Where(i => i))
                e.passiva?.Unsubscribe(this);
        }

        private void GetSkills() {
            skills = member.equipedSkills.Where(s => s && s.animation != null).ToList();

            foreach (var e in equipment)
                if(!skills.Contains(e.equipmentSkill))
                    skills.Add(e.equipmentSkill);

            basicAttack = equipment.OfType<Weapon>().FirstOrDefault()?.basicAttack;
            basicAttack ??= profession.BasicAttack;
        }
    }
}