using TurnBasedRPG.Data;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(menuName = "Scriptable/Item/Consumable", fileName = "New Consumable")]
    public class Consumable : Item, IConsumable {
        public ISkill skillEffect;
        public ISkill Skill => skillEffect;
    }
}
