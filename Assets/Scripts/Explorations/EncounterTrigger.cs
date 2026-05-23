using System.Collections;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.UI.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace Explorations {
    public class EncounterTrigger : MonoBehaviour {
        private static readonly int AlphaID = Shader.PropertyToID("_alpha");
        private static readonly int PixelsID = Shader.PropertyToID("_pixels");

        public CombatManager combatManager;
        public EnemyEncounter encounter;
        public CombatArena arena;
        
        public Image staticEffect;
        public Vector2 duration = new(.5f, .25f);
        public Vector2 pixels = new(10, 40);

        private void Awake() {
            arena.Camera.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other) {
            if(!other.TryGetComponent(out PlayerParty p)) return;

            var config = new CombatConfig
            {
                Arena = arena,
                Allies = p.members,
                Enemies = encounter.enemyList,
                Items = p.consumables.slots,
            };

            StartCoroutine(StaticTransition(config));
        }

        private IEnumerator StaticTransition(CombatConfig config) {
            ExplorationTask.Add(this);
            var material = staticEffect.material;
            
            staticEffect.gameObject.SetActive(true);
            material.SetFloat(AlphaID, 0);
            material.SetFloat(PixelsID, 10);

            for (var i = 0f; i < 1f; i += Time.unscaledDeltaTime / duration.x) {
                material.SetFloat(AlphaID, i);
                material.SetFloat(PixelsID, pixels.x + pixels.y * i);
                yield return null;
            }

            material.SetFloat(AlphaID, 1);
            material.SetFloat(PixelsID, pixels.x + pixels.y);

            combatManager.StartCombat(config);
            combatManager.Pause = true;
            yield return new WaitForSecondsRealtime(duration.y);
            
            for (var i = 0f; i < 1f; i += Time.unscaledDeltaTime / duration.x) {
                material.SetFloat(AlphaID, 1-i);
                material.SetFloat(PixelsID, pixels.x + pixels.y * (1-i));
                yield return null;
            }

            combatManager.Pause = false;
            staticEffect.gameObject.SetActive(false);
            ExplorationTask.Remove(this);
        }
    }
}