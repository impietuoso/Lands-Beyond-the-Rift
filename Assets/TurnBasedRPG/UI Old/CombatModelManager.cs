using System.Collections.Generic;
using TurnBasedRPG.UI_Review;
using UnityEngine;

namespace TurnBasedRPG.UI {
    public class CombatModelManager : DataView<CombatManager> {
        public CharacterFeedbacks prefab;
        public Transform[] allyPositions;
        public Transform[] enemyPositions;

        private readonly Dictionary<Character, CharacterFeedbacks> _models = new();

        protected override void Subscribe() {
            for (var i = 0; i < Data.allies.Count; i++) {
                if(i >= allyPositions.Length) break;
                var instance = Instantiate(prefab, allyPositions[i]);
                instance.SetData(Data.allies[i]);
                _models.Add(Data.allies[i], instance);
            }

            for (var i = 0; i < Data.enemies.Count; i++) {
                if(i >= enemyPositions.Length) break;
                var instance = Instantiate(prefab, enemyPositions[i]);
                instance.SetData(Data.enemies[i]);
                _models.Add(Data.enemies[i], instance);
            }
        }

        protected override void Unsubscribe() {
            foreach (var model in _models.Values)
                Destroy(model.gameObject);
            _models.Clear();
        }

        public Vector3 GetCharacterPosition(Character character) {
            return _models.TryGetValue(character, out var feedback)
                ? feedback.transform.position
                : Vector3.zero;
        }

        public void ClearTargets() {
            foreach (var model in _models.Values)
                model.TargetMark.SetActive(false);
        }

        public void TargetCharacters(IEnumerable<Character> targets) {
            foreach (var target in targets)
                if(_models.TryGetValue(target, out var feedback))
                    feedback.TargetMark.SetActive(true);
        }
    }
}