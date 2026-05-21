using UnityEngine;

namespace TurnBasedRPG.DrawerHelpers {
    public class LabelAttribute : PropertyAttribute {
        public string Label { get; }
        public LabelAttribute(string label) => Label = label;
    }
}