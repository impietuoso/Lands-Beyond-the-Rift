using System.Collections.Generic;
using TurnBasedRPG.Data;
using UnityEngine;

namespace Data {
    public abstract class Item : ScriptableObject, IItem {
        [field: SerializeField] public string displayName { get; protected set; }
        [field: SerializeField] public Sprite sprite { get; protected set; }
        [field: TextArea(3, 6)]
        [field: SerializeField] public string description { get; protected set; }
        [SerializeField] private Tag[] tags;

        public IReadOnlyList<Tag> Tags => tags;

        public virtual int maxStack => 99;
    }
}