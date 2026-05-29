using System.Collections.Generic;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.Data;
using UnityEngine;

namespace Data {
    [CreateAssetMenu(menuName = "Scriptable/Profession", fileName = "New Profession")]
    public class Profession : ScriptableObject, IAttributes {
        [SerializeField] private Attributes attributeBonus;
        [SerializeField] private Skill basicAttack;
        [SerializeField] private List<Skill> starterSkills = new();
        [SerializeField] private List<Skill> unlockableSkills = new();

        public ISkill BasicAttack => basicAttack;
        public IReadOnlyList<ISkill> StarterSkills => starterSkills;
        public IReadOnlyList<ISkill> UnlockableSkills => unlockableSkills;

        public int this[Attribute a] => attributeBonus[a];
    }
}