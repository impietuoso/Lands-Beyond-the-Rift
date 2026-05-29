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
            pName = member.CharName;
            Allies = allies;
            Enemies = enemies;
            Element = member.Element;
            basicAttack = member.BasicAttack;
            skills = member.GetSkills().ToList();
            
            InitializeStats();
            OnSetup?.Invoke(this);
        }

        public CombatManager cm { get; }
        public IReadOnlyList<Character> Allies { get; }
        public IReadOnlyList<Character> Enemies { get; }

        [Header("Advancement Info")]
        [SerializeField, HideInInspector] private string pName;
        public IPartyMember member;
        public string team;
        public Element Element;
        public ISkill basicAttack;
        public List<ISkill> skills;
        public StatusEffectList StatusEffectList;

        public string characterName => member.CharName;
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
            foreach (var p in member.GetPassives())
                p?.Subscribe(this);
        }

        public void UnsubscribePassives() {
            foreach (var p in member.GetPassives())
                p?.Unsubscribe(this);
        }
    }
}