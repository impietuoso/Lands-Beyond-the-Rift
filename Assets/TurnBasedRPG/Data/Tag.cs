using UnityEngine;

namespace TurnBasedRPG.Data
{
    [CreateAssetMenu(menuName = "Scriptable/Tag", fileName = "New Tag")]
    public class Tag : ScriptableObject {
        public string tagName;
    }
}