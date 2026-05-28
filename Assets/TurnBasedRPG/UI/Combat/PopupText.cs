using TMPro;
using UnityEngine;

namespace TurnBasedRPG.UI.Combat
{
    public class PopupText : MonoBehaviour {
        public TextMeshProUGUI textcanvas;
        public TextMeshPro text;

        public void Message(string message, Color color) {
            if (textcanvas) {
                textcanvas.text = message;
                textcanvas.color = color;
            } else if (text) {
                text.text = message;
                text.color = color;
            }
        }

        public void AutoDestroy() {
            Destroy(gameObject);
        }
    }
}