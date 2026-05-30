using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.RectTransform.Axis;

namespace TurnBasedRPG.UI.Views {
    public class ShrinkButtonsAnim : MonoBehaviour {
        [SerializeField] private VerticalLayoutGroup layout;
        [SerializeField] private float duration = .25f;
        [SerializeField] private RectTransform cancel;
        [SerializeField] private RectTransform[] buttons;

        private float _progress;
        private bool _shrinking;
        private float _height;

        private void Awake() {
            _height = buttons[0].rect.height;
        }

        public void Shrink() {
            _shrinking = true;
            enabled = true;
        }

        public void Grow() {
            _shrinking = false;
            enabled = true;
        }

        private void Update() {
            var delta = _shrinking ? Time.unscaledDeltaTime : -Time.unscaledDeltaTime;
            _progress += delta / duration;
            _progress = Mathf.Clamp01(_progress);

            cancel.SetSizeWithCurrentAnchors(Vertical, _height * _progress);
            foreach (var button in buttons)
                button.SetSizeWithCurrentAnchors(Vertical, _height * (1 - _progress));

            if(_progress == 0 || _progress == 1) enabled = false;
        }
    }
}