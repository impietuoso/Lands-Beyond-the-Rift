using UnityEngine;

namespace Explorations {
    public class Interactor : MonoBehaviour {
        public Interactable currentInteraction;

        public void TryInteract() {
            if (!enabled) return;
            if(currentInteraction != null)
                currentInteraction.interaction.Invoke();
        }
    }
}