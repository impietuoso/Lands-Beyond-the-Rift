using System.Collections.Generic;
using TurnBasedRPG.BattleStats;
using UnityEngine;

namespace TurnBasedRPG.Data
{
    [CreateAssetMenu(menuName = "Scriptable/Profession", fileName = "New Profession")]
    public class Profession : ScriptableObject, IAttributes
    {
        [SerializeField] private Attributes attributeBonus;
        [SerializeField] private Skill basicAttack;
        [SerializeField] private List<Skill> starterSkills = new();
        [SerializeField] private List<Skill> unlockableSkills = new();

        public Skill BasicAttack => basicAttack;
        public IReadOnlyList<Skill> StarterSkills => starterSkills;
        public IReadOnlyList<Skill> UnlockableSkills => unlockableSkills;

        public int this[Attribute a] => attributeBonus[a];
    }
}