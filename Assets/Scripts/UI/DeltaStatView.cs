using TMPro;
using TurnBasedRPG.BattleStats;
using UnityEngine;

namespace UI {
    public class DeltaStatView : DataView<(int v, int d)> {
        [SerializeField] private Stat stat;
        [SerializeField] private string alias;
        [SerializeField] private TMP_Text text;
        [SerializeField] private Color addColor, subColor;

        public Stat Stat => stat;

        protected override void Subscribe() {
            text.text = $"{alias}: {Data.v}";
            if(Data.d == 0) return;

            var color = Data.d > 0 ? addColor : subColor;
            var colorCode = ColorUtility.ToHtmlStringRGB(color);
            var sign = Data.d > 0 ? "+" : "";
            text.text += $" <color=#{colorCode}>{sign}{Data.d}</color>";
        }

        protected override void Unsubscribe() { }
    }
}