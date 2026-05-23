using UnityEngine;
using UnityEngine.Events;

namespace Explorations {
    public class ExplorationTaskListener : MonoBehaviour {
        [field: SerializeField] public UnityEvent OnFree { get; private set; }
        [field: SerializeField] public UnityEvent OnBusy { get; private set; }

        private void OnEnable() {
            ExplorationTask.OnBusyChanged += HandleBusyChanged;
            HandleBusyChanged(ExplorationTask.IsBusy);
        }

        private void OnDisable() {
            ExplorationTask.OnBusyChanged -= HandleBusyChanged;
        }

        private void HandleBusyChanged(bool isBusy) {
            if (isBusy) OnBusy?.Invoke();
            else OnFree?.Invoke();
        }
    }
}
