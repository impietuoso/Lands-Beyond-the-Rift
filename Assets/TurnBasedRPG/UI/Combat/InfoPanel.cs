using TMPro;
using UnityEngine;

namespace TurnBasedRPG.UI.Combat {
    public class InfoPanel : MonoBehaviour {
        [SerializeField] private TMP_Text titleText;
        [SerializeField] private TMP_Text infoText;

        public void Show(string info) {
            infoText.text = info;
            gameObject.SetActive(true);
        }

        public void Show(string title, string info) {
            titleText.text = title;
            Show(info);
        }

        public void Hide() => gameObject.SetActive(false);
    }
}