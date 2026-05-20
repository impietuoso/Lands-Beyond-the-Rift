using System.Linq;
using System.Text;
using TMPro;
using TurnBasedRPG.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedRPG.UI.Views
{
    public class PartyMemberView : DataView<PartyMember>
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI professionText;
        public TextMeshProUGUI allStatsText;
        public Image icon;
        public ListView equipedSkills;
        public ListView avaliableSkills;
        public ListView equipments;
        public CanvasGroup interactable;
        private Stats statsPreview = new();

        private void Start()
        {
            if (interactable && !Data) interactable.interactable = false;
        }

        private string AllStats()
        {
            statsPreview.Recalculate(Data.level, Data, Data);

            var all = new StringBuilder();

            foreach (var a in Attributes.All)
            {
                all.Append(a);
                all.Append(" - ");
                all.AppendLine(Data[a].ToString());
            }

            foreach (var s in Stats.All)
            {
                all.Append(s);
                all.Append(" - ");
                all.AppendLine(Data[s].ToString());
            }

            return all.ToString();
        }

        protected override void Subscribe()
        {
            if (nameText) nameText.text = Data.charName;
            if (levelText) levelText.text = $"Lv. {Data.level}";
            if (professionText) professionText.text = Data.profession.name;
            if (allStatsText) allStatsText.text = AllStats();
            if (equipedSkills) equipedSkills.SetData(Data.equipedSkills);
            if (avaliableSkills) avaliableSkills.SetData(Data.learnedSkills);
            if (equipments)
            {
                var sortedEquips = GameConfig.Instance.equipmentDrawOrder
                    .Select(type => Data.equips[type]);
                equipments.SetData(sortedEquips);
            }

            if (icon) icon.overrideSprite = Data.uiSprite;
            if (interactable) interactable.interactable = true;
        }

        protected override void Unsubscribe()
        {
            if (interactable) interactable.interactable = false;
            if (nameText) nameText.text = "Empty";
            if (levelText) levelText.text = "";
            if (professionText) professionText.text = "-";
            if (icon) icon.overrideSprite = null;
        }
    }
}