using System;
using System.Collections;
using UnityEngine;

namespace DialogueSystem.DialogueEffects {
    [Serializable]
    public class ShakeDialogueBox : IDialogueEffect {
        [SerializeField] public float magnitude = 12f;
        [SerializeField] public float duration = 1.2f;
        public IEnumerator Play(DialogueSystem sys) {
            Transform target = sys.dialoguePanel.transform;
            Vector3 originalPos = target.localPosition;
            float elapsed = 0.0f;

            while (elapsed < duration) {
                float percentComplete = elapsed / duration;
                float damper = 1.0f - percentComplete;
                
                float x = Mathf.Sin(elapsed * 50f) * magnitude * damper;
                float y = Mathf.Cos(elapsed * 50f) * magnitude * damper;

                target.localPosition = new Vector3(originalPos.x + x, originalPos.y + y, originalPos.z);
                
                elapsed += Time.deltaTime;
                yield return null;
            }

            target.localPosition = originalPos;
        }
    }
}
