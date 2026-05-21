using TMPro;
using UnityEngine;

namespace TurnBasedRPG.UI
{
    public class PopupText : MonoBehaviour {
        public TextMeshProUGUI text;

        public void Message(string message, Color color) {
            text.text = message;
            text.color = color;
        }

        public void AutoDestroy() {
            Destroy(gameObject);
        }
    }
}