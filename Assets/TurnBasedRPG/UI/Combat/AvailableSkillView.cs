using TMPro;
using TurnBasedRPG.Data;
using UnityEngine.UI;

namespace TurnBasedRPG.UI.Combat {
    public class AvailableSkillView : DataView<(Character c, Skill s)> {
        public TMP_Text displayName;
        public TMP_Text cost;
        public Image icon;
        public Button button;

        protected override void Subscribe() {
            displayName.text = Data.s.skillName;
            cost.text = Data.s.cost.ToString();
            icon.overrideSprite = Data.s.icon;
            button.interactable = Data.s.Available(Data.c);
        }

        protected override void Unsubscribe() { }
    }
}