using UnityEngine;

namespace TurnBasedRPG.UI
{
    public class CurrentAction : MonoBehaviour {
        public void Disable() {
            gameObject.SetActive(false);
        }
    }
}
