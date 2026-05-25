using TMPro;
using TurnBasedRPG.Data;
using TurnBasedRPG.StatusEffects;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedRPG.UI.Views
{
    public class ListTemplateView : MonoBehaviour {
        public Image icon;
        public TMP_Text nameText;
        public TMP_Text extraText;

        public void UpdateTemplateUI(ISkill skill) {
            if(icon) icon.sprite = skill.Icon;
            if(nameText) nameText.text = skill.SkillName;
            if(extraText) extraText.text = skill.Cost + "\n <size=14>MP</size>";
        }
    
        public void UpdateTemplateUI(Status status) {
            if(icon) icon.sprite = status.Source.Icon;
            if(nameText) nameText.text = status.Source.DisplayName;
            if(extraText) extraText.text = status.Source.Description;
        }
    }
}