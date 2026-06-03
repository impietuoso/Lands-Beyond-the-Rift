using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem.DialogueEffects {
    [Serializable]
    public class FlashRed : IDialogueEffect {
        public enum FlashTarget { Left, Right, Both }
        [SerializeField] public float duration = 0.5f;
        [SerializeField] public int flashes = 2;
        [SerializeField] public FlashTarget target = FlashTarget.Left;

        public IEnumerator Play(DialogueSystem sys) {
            List<Image> targets = new List<Image>();

            switch (target) {
                case FlashTarget.Left:
                    if (sys.leftPortrait != null) targets.Add(sys.leftPortrait);
                    break;
                case FlashTarget.Right:
                    if (sys.rightPortrait != null) targets.Add(sys.rightPortrait);
                    break;
                case FlashTarget.Both:
                    if (sys.leftPortrait != null) targets.Add(sys.leftPortrait);
                    if (sys.rightPortrait != null) targets.Add(sys.rightPortrait);
                    break;
            }

            if (targets.Count == 0) yield break;

            List<Color> originalColors = new List<Color>();
            foreach (var img in targets) {
                originalColors.Add(img.color);
            }

            float elapsed = 0f;
            while (elapsed < duration) {
                elapsed += Time.deltaTime;
                float phase = (elapsed / duration) * flashes * 2.0f * Mathf.PI;
                float lerp = (Mathf.Sin(phase - Mathf.PI / 2f) + 1f) / 2f;
                
                for (int i = 0; i < targets.Count; i++) {
                    targets[i].color = Color.Lerp(originalColors[i], Color.red, lerp);
                }
                yield return null;
            }

            for (int i = 0; i < targets.Count; i++) {
                targets[i].color = originalColors[i];
            }
        }
    }
}
