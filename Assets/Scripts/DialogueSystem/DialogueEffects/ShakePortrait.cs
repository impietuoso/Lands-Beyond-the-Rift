using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace DialogueSystem.DialogueEffects {
    [Serializable]
    public class ShakePortrait : IDialogueEffect {
        public enum ShakeTarget { Left, Right, Both }
        [SerializeField] public float magnitude = 12f;
        [SerializeField] public float duration = 1.2f;
        [SerializeField] public ShakeTarget target = ShakeTarget.Left;

        public IEnumerator Play(DialogueSystem sys) {
            List<Transform> targets = new List<Transform>();

            switch (target) {
                case ShakeTarget.Left:
                    if (sys.leftPortrait != null) targets.Add(sys.leftPortrait.transform);
                    break;
                case ShakeTarget.Right:
                    if (sys.rightPortrait != null) targets.Add(sys.rightPortrait.transform);
                    break;
                case ShakeTarget.Both:
                    if (sys.leftPortrait != null) targets.Add(sys.leftPortrait.transform);
                    if (sys.rightPortrait != null) targets.Add(sys.rightPortrait.transform);
                    break;
            }

            if (targets.Count == 0) yield break;

            List<Vector3> originalPositions = new List<Vector3>();
            foreach (var t in targets) {
                originalPositions.Add(t.localPosition);
            }

            float elapsed = 0.0f;

            while (elapsed < duration) {
                float percentComplete = elapsed / duration;
                float damper = 1.0f - percentComplete;
                
                float x = Mathf.Sin(elapsed * 50f) * magnitude * damper;
                float y = Mathf.Cos(elapsed * 50f) * magnitude * damper;

                for (int i = 0; i < targets.Count; i++) {
                    targets[i].localPosition = new Vector3(originalPositions[i].x + x, originalPositions[i].y + y, originalPositions[i].z);
                }
                
                elapsed += Time.deltaTime;
                yield return null;
            }

            for (int i = 0; i < targets.Count; i++) {
                targets[i].localPosition = originalPositions[i];
            }
        }
    }
}
