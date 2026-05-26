using TurnBasedRPG.Data;
using UnityEngine;

namespace RPG {
    [CreateAssetMenu(menuName = "Scriptable/Item/Weapon", fileName = "New Weapon")]
    public class Weapon : Equipment, IWeapon {
        [SerializeField] private Skill attack;
        public ISkill Attack => attack;
    }
}