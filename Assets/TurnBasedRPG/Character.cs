using System;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.Data;
using TurnBasedRPG.Inventory;
using TurnBasedRPG.StatusEffect;
using UnityEngine;

namespace TurnBasedRPG
{
    [Serializable]
    public partial class Character
    {
        public Character(PartyMember member, string newTeam)
        {
            this.member = member;
            team = newTeam;
            Element = member.element;

            GetSkills();
            InitializeStats();
            OnSetup?.Invoke(this);
        }

        public Character() { }

        [Header("Advancement Info")]
        public PartyMember member;
        public string team;
        public Element Element;
        public List<Skill.Skill> basicAttack = new();
        public List<Skill.Skill> skills = new();
        public StatusEffectList StatusEffectList;

        public IObservableList<Equipment> equipment => member.equips;
        public string characterName => member.charName;
        public Profession profession => member.profession;
        public Sprite characterSprite => member.characterSprite;
        public Sprite uiSprite => member.uiSprite;

        public Action<Character> OnSetup;
        public Action<Character> OnStartTurn;
        public Action<Character> OnEndTurn;
        public Action<CombatArgs> OnDefend;
        public Action<CombatArgs> OnAttack;
        public Action<CombatArgs> OnResolveDefend;
        public Action<CombatArgs> OnResolveAttack;

        public Observable<float> actionPoints = new();

        public void SubscribePassives()
        {
            foreach (var e in member.equips.Where(i => i))
                e.passiva?.Subscribe(this);

            foreach (var s in member.equipedSkills.Where(i => i))
                s.passiva?.Subscribe(this);
        }

        public void UnsubscribePassives()
        {
            foreach (var e in member.equips.Where(i => i))
                e.passiva?.Unsubscribe(this);

            foreach (var s in member.equipedSkills.Where(i => i))
                s.passiva?.Unsubscribe(this);
        }

        private void GetSkills()
        {
            skills = member.equipedSkills.Where(s => s && s.animation != null).ToList();

            foreach (var e in equipment)
            {
                if (e is Weapon w && w.basicAttack)
                    if (!basicAttack.Contains(w.basicAttack))
                        basicAttack.Add(w.basicAttack);

                if (!skills.Contains(e.equipmentSkill))
                    skills.Add(e.equipmentSkill);
            }

            if (basicAttack.Count == 0)
                basicAttack.Add(profession.BasicAttack);
        }
    }
}