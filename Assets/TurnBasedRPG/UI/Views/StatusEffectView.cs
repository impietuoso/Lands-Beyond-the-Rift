using TMPro;
using TurnBasedRPG.StatusEffects;
using UnityEngine;
using UnityEngine.UI;

namespace TurnBasedRPG.UI.Views
{
    public class StatusEffectView : MonoBehaviour {
        public Image statusIcon;
        public TextMeshProUGUI durationText;
        public TextMeshProUGUI nameText;
        public TextMeshProUGUI descriptionText;
        private Status owner;

        public void SetInfo(Status status) {
            owner = status;
            statusIcon.sprite = status.Source.Icon;
            if (durationText) durationText.text = status.DisplayValue.Value.ToString();
            if (nameText) nameText.text = status.Source.DisplayName;
            if (descriptionText) descriptionText.text = status.Source.Description;
            status.DisplayValue.OnChange += UpdateUI;
        }

        private void OnDestroy() {
            if(owner!= null) owner.DisplayValue.OnChange -= UpdateUI;
        }

        public void UpdateUI(int i) {
            if(durationText) durationText.text = i.ToString();
        }
    }
}
