using UnityEngine;

namespace TurnBasedRPG.Inventory
{
    [CreateAssetMenu(menuName = "Scriptable/Item/Weapon", fileName = "New Weapon")]
    public class Weapon : Equipment {
        public Skill.Skill basicAttack;
    }
}
