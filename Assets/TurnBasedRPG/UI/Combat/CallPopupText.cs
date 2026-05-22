using System.Collections;
using UnityEngine;

namespace TurnBasedRPG.UI.Combat
{
    public class CallPopupText : MonoBehaviour {
        public PopupText popup;
    
        public IEnumerator Pop(string message, Color color, Transform target, float delay) {
            yield return new WaitForSeconds(delay);
            var newPopup = Instantiate(popup, target.position, Quaternion.identity, target);
            newPopup.GetComponent<PopupText>().Message(message, color);
            newPopup.gameObject.SetActive(true);
        }
    }
}