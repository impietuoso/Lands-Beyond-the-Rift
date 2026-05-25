using TMPro;
using TurnBasedRPG.Data;
using UnityEngine.UI;

namespace TurnBasedRPG.UI.Combat {
    public class AvailableSkillView : DataView<(Character c, ISkill s)> {
        public TMP_Text displayName;
        public TMP_Text cost;
        public Image icon;
        public Button button;

        protected override void Subscribe() {
            displayName.text = Data.s.SkillName;
            cost.text = Data.s.Cost.ToString();
            icon.overrideSprite = Data.s.Icon;
            button.interactable = Data.s.Available(Data.c);
        }

        protected override void Unsubscribe() { }
    }
}