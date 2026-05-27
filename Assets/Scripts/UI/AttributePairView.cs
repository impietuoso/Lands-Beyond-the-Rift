using TMPro;
using TurnBasedRPG.BattleStats;
using UnityEngine;

namespace RPG {
    public class AttributePairView : DataView<(Attribute a, int v)> {
        [SerializeField] private TMP_Text text;
        protected override void Subscribe() => text.text = $"{Data.a} - {Data.v}";
        protected override void Unsubscribe() { }
    }
}