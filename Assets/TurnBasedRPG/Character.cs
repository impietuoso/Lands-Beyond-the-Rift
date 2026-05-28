using System;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.Data;
using TurnBasedRPG.StatusEffects;
using UnityEngine;

namespace TurnBasedRPG {
    [Serializable]
    public partial class Character {
        public Character(CombatManager cm, IPartyMember member, string teamName,
            List<Character> allies, List<Character> enemies) {
            this.cm = cm;
            this.member = member;
            team = teamName;
            Allies = allies;
            Enemies = enemies;
            Element = member.Element;

            GetSkills();
            InitializeStats();
            OnSetup?.Invoke(this);
        }

        public CombatManager cm { get; }
        public IReadOnlyList<Character> Allies { get; }
        public IReadOnlyList<Character> Enemies { get; }

        [Header("Advancement Info")]
        public IPartyMember member;
        public string team;
        public Element Element;
        public ISkill basicAttack;
        public List<ISkill> skills = new();
        public StatusEffectList StatusEffectList;

        public IReadOnlyList<IEquipment> equipment => member.Equips;
        public string characterName => member.CharName;
        public Profession profession => member.Profession;
        public bool Dead => Health.Current == 0;
        public Vector3 Position => cm.Arena.GetPosition(this);

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
            foreach (var e in member.Equips)
                e?.Passive?.Subscribe(this);
        }

        public void UnsubscribePassives() {
            foreach (var e in member.Equips)
                e?.Passive?.Unsubscribe(this);
        }

        private void GetSkills() {
            skills = member.EquipedSkills.Where(s => s?.Animation != null).ToList();

            foreach (var e in equipment)
                if(e?.ActiveSkill != null && !skills.Contains(e.ActiveSkill))
                    skills.Add(e.ActiveSkill);

            basicAttack = equipment.OfType<IWeapon>().FirstOrDefault()?.Attack;
            basicAttack ??= member.BasicAttack;
        }
    }
}