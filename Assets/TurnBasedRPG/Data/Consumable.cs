using UnityEngine;

namespace TurnBasedRPG.Inventory
{
    [CreateAssetMenu(menuName = "Scriptable/Item/Consumable", fileName = "New Consumable")]
    public class Consumable : Item {
        public Skill.Skill skillEffect;
    }
}
