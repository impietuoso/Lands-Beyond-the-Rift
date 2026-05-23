using UnityEngine;
using UnityEngine.Events;

namespace Explorations {
    public class Interactable : MonoBehaviour {
        public GameObject interactIcon;
        public UnityEvent interaction;

        private void OnTriggerEnter2D(Collider2D col) {
            if (!col.GetComponent<Interactor>()) return;
            interactIcon.SetActive(true);
            col.GetComponent<Interactor>().currentInteraction = this;
        }

        private void OnTriggerExit2D(Collider2D col) {
            if (!col.GetComponent<Interactor>()) return;
            interactIcon.SetActive(false);
            if(col.GetComponent<Interactor>().currentInteraction == this)
                col.GetComponent<Interactor>().currentInteraction = null;
        }
    }
}