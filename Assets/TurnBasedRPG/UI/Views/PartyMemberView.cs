using System.Text;
using TMPro;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.Data;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedRPG.UI.Views
{
    public class PartyMemberView : DataView<IPartyMember>
    {
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI levelText;
        public TextMeshProUGUI allStatsText;
        public Image icon;
        public ListView equipedSkills;
        public ListView avaliableSkills;
        public ListView equipments;
        public CanvasGroup interactable;
        private Stats statsPreview = new();

        private void Start()
        {
            if (Data == null) interactable.interactable = false;
        }

        private string AllStats()
        {
            statsPreview.Recalculate(Data.Level, Data, Data);

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
            if (nameText) nameText.text = Data.CharName;
            if (levelText) levelText.text = $"Lv. {Data.Level}";
            if (allStatsText) allStatsText.text = AllStats();
            if (equipedSkills) equipedSkills.SetData(Data.EquipedSkills);
            if (avaliableSkills) avaliableSkills.SetData(Data.LearnedSkills);
            if (equipments) equipments.SetData(Data.Equips);
            if (icon) icon.overrideSprite = Data.UISprite;
            if (interactable) interactable.interactable = true;
        }

        protected override void Unsubscribe()
        {
            if (interactable) interactable.interactable = false;
            if (nameText) nameText.text = "Empty";
            if (levelText) levelText.text = "";
            if (icon) icon.overrideSprite = null;
        }
    }
}