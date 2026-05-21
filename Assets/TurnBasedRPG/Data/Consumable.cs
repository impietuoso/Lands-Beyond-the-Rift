using UnityEngine;

namespace TurnBasedRPG.Data
{
    [CreateAssetMenu(menuName = "Scriptable/Item/Consumable", fileName = "New Consumable")]
    public class Consumable : Item {
        public Skill skillEffect;
    }
}
