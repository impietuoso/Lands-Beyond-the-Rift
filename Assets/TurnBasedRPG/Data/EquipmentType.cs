using UnityEngine;

namespace TurnBasedRPG.Inventory
{
    [CreateAssetMenu(menuName = "Scriptable/Item/EquipmentType", fileName = "New Equipment Type")]
    public class EquipmentType : ScriptableObject {
        public string typeName;
        public Sprite nothingEquipedSprite;
    }
}
