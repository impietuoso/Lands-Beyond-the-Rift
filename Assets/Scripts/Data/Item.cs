using UnityEngine;

namespace RPG {
    public abstract class Item : ScriptableObject, IItem {
        [field: SerializeField] public string displayName { get; protected set; }
        [field: SerializeField] public Sprite sprite { get; protected set; }
        [field: TextArea(3, 6)]
        [field: SerializeField] public string description { get; protected set; }
        public virtual int maxStack => 99;
    }
}