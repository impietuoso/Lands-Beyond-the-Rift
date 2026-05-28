using UnityEngine;
using UnityEngine.Events;

namespace Guild_Wars_Engine {
    public class TagTriggerEnter : MonoBehaviour {
        public string tagName = "Player";
        public UnityEvent onTrigger;

        private void OnTriggerEnter2D(Collider2D other) {
            if(other.CompareTag(tagName)) onTrigger?.Invoke();
        }
    }
}