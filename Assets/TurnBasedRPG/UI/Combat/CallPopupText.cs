using System.Collections;
using UnityEngine;

namespace TurnBasedRPG.UI.Combat
{
    public class CallPopupText : MonoBehaviour {
        public PopupText popup;
    
        public IEnumerator Pop(string message, Color color, Vector2 position, float delay) {
            yield return new WaitForSeconds(delay);
            var newPopup = Instantiate(popup, position, Quaternion.identity);
            newPopup.GetComponent<PopupText>().Message(message, color);
            newPopup.gameObject.SetActive(true);
        }
    }
}