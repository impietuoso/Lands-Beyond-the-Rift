using TMPro;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.UI.Views;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedRPG.Controller
{
    public class AttributeManager : DataView<Attribute>
    {
        public AttributesManager manager;
        public PartyMemberView member;
        public TMP_Text attributeName;
        public Sprite attributeIcon;

        [Header("UI")]
        public TextMeshProUGUI valueText;
        public Button upButton;
        public Button downButton;

        protected override void Subscribe()
        {
            attributeName.text = Data.ToString();
        }

        protected override void Unsubscribe() { }

        public void LevelUp()
        {
            if (member.Data.GetUnusedPoints() > 0)
                member.Data.UsedAttributes[Data]++;
            manager.UpdateStatsValue();
        }

        public void LevelDown()
        {
            if (member.Data.UsedAttributes[Data] > 0)
                member.Data.UsedAttributes[Data]--;
            manager.UpdateStatsValue();
        }
    }
}