using System.Linq;
using TMPro;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.UI.Views;
using UnityEngine;

namespace TurnBasedRPG.Controller
{
    public class AttributesManager : MonoBehaviour {
        public PartyMemberView memberView;
        public ListView attributesView;
        public TextMeshProUGUI availablePointsText;
        
        private Stats _previewStats;

        private void Start() => attributesView.SetData(Attributes.All);

        public void CheatLevelUp() {
            if(memberView.Data.level < 20) 
                memberView.Data.level++;
            UpdateStatsValue();
        }

        public void CheatLevelDown() {
            if(memberView.Data.level > 1)
                memberView.Data.level--;
            UpdateStatsValue();
        }

        public void UpdateStatsValue() {
            var member = memberView.Data;
            var pointsLeft = member.GetUnusedPoints() ;
            _previewStats.Recalculate(member.level, member, member);
            
            availablePointsText.text = pointsLeft.ToString();
            foreach (var view in attributesView.templateList.OfType<AttributeManager>()) {
                view.upButton.interactable = pointsLeft > 0;
                view.downButton.interactable = member.usedAttributes[view.Data] > 0;
                view.valueText.text = member[view.Data].ToString();
            }
        }
    }
}