using System.Collections.Generic;
using TurnBasedRPG.DrawerHelpers.Search;
using UnityEngine;

namespace TurnBasedRPG.UI.Combat {
    public class CombatArena : DataView<CombatManager> {
        [SerializeField, Prefab] private CharacterModel prefab;
        [field: SerializeField] public GameObject Camera { get; private set; }
        [SerializeField] private Transform center;
        [SerializeField] private Transform[] allyPositions;
        [SerializeField] private Transform[] enemyPositions;

        public Vector3 Center => center.position;

        private readonly Dictionary<Character, CharacterModel> _models = new();

        protected override void Subscribe() {
            for (var i = 0; i < Data.Allies.Count; i++) {
                if(i >= allyPositions.Length) break;
                var instance = Instantiate(prefab, allyPositions[i]);
                instance.SetData(Data.Allies[i]);
                _models.Add(Data.Allies[i], instance);
            }

            for (var i = 0; i < Data.Enemies.Count; i++) {
                if(i >= enemyPositions.Length) break;
                var instance = Instantiate(prefab, enemyPositions[i]);
                instance.SetData(Data.Enemies[i]);
                _models.Add(Data.Enemies[i], instance);
            }
        }

        protected override void Unsubscribe() {
            foreach (var model in _models.Values)
                Destroy(model.gameObject);
            _models.Clear();
        }

        public Vector3 GetPosition(Character character) {
            return _models.TryGetValue(character, out var feedback)
                ? feedback.transform.position
                : throw new MissingReferenceException();
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