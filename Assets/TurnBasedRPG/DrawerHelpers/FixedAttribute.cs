using UnityEngine;

namespace TurnBasedRPG.DrawerHelpers {
    public class FixedAttribute : PropertyAttribute {
        public int Size { get; }
        public FixedAttribute(int size) => Size = size;
    }
}