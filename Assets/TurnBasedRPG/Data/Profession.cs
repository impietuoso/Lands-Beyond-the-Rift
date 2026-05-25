using System.Collections.Generic;
using TurnBasedRPG.BattleStats;
using UnityEngine;

namespace TurnBasedRPG.Data
{
    [CreateAssetMenu(menuName = "Scriptable/Profession", fileName = "New Profession")]
    public class Profession : ScriptableObject, IAttributes
    {
        [SerializeField] private Attributes attributeBonus;
        [SerializeField] private ISkill basicAttack;
        [SerializeField] private List<ISkill> starterSkills = new();
        [SerializeField] private List<ISkill> unlockableSkills = new();

        public ISkill BasicAttack => basicAttack;
        public IReadOnlyList<ISkill> StarterSkills => starterSkills;
        public IReadOnlyList<ISkill> UnlockableSkills => unlockableSkills;

        public int this[Attribute a] => attributeBonus[a];
    }
}