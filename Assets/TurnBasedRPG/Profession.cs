using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG
{
    [CreateAssetMenu(menuName = "Scriptable/Profession", fileName = "New Profession")]
    public class Profession : ScriptableObject, IAttributes
    {
        [SerializeField] private Attributes attributeBonus;
        [SerializeField] private Skill.Skill basicAttack;
        [SerializeField] private List<Skill.Skill> starterSkills = new();
        [SerializeField] private List<Skill.Skill> unlockableSkills = new();

        public Skill.Skill BasicAttack => basicAttack;
        public IReadOnlyList<Skill.Skill> StarterSkills => starterSkills;
        public IReadOnlyList<Skill.Skill> UnlockableSkills => unlockableSkills;

        public int this[Attribute a] => attributeBonus[a];
    }
}