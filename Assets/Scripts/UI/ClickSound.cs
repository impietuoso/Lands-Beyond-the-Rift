using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI {
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Selectable))]
    public class ClickSound : MonoBehaviour, IPointerClickHandler {
        private Selectable _item;

        private void Awake() => _item = GetComponent<Selectable>();

        public void OnPointerClick(PointerEventData eventData) {
            var sfx = _item.IsInteractable() ? Game.Settings.ClickSfx : Game.Settings.ClickBlockSfx;
            Game.Audio.PlaySFX(sfx);
        }

#if UNITY_EDITOR
        [UnityEditor.MenuItem("Tool/Add Click Sounds to Hierarchy")]
        private static void AddClickSounds() {
            var selection = UnityEditor.Selection.activeGameObject;
            if(!selection) return;

            foreach (var selectable in selection.GetComponentsInChildren<Selectable>(true)) {
                if(selectable.interactable == false) continue;
                if(selectable.GetComponent<ClickSound>()) continue;
                UnityEditor.Undo.AddComponent<ClickSound>(selectable.gameObject);
                Debug.Log("sound added to " + selectable.name, selectable);
            }
        }
#endif
    }
}