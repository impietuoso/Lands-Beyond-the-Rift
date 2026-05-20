using UnityEngine;

namespace TurnBasedRPG.Inventory
{
    [CreateAssetMenu(menuName = "Scriptable/Tag", fileName = "New Tag")]
    public class Tag : ScriptableObject {
        public string tagName;
    }
}