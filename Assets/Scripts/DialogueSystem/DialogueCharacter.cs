using UnityEngine;

namespace DialogueSystem {
    [CreateAssetMenu(menuName = "Scriptable/Dialogue Character", fileName = "New Dialogue Character")]
    public class DialogueCharacter : ScriptableObject {
        public string displayName;
        public Sprite portrait;
    }
}
