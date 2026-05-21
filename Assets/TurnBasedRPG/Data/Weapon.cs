using UnityEngine;

namespace TurnBasedRPG.Data
{
    [CreateAssetMenu(menuName = "Scriptable/Item/Weapon", fileName = "New Weapon")]
    public class Weapon : Equipment {
        public Skill basicAttack;
    }
}
