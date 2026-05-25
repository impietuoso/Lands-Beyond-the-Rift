using System.Collections;
using RPG;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.UI.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace Explorations {
    public class EncounterTrigger : MonoBehaviour {
        private static readonly int AlphaID = Shader.PropertyToID("_alpha");
        private static readonly int PixelsID = Shader.PropertyToID("_pixels");

        public Encounter encounter;
        public CombatArena arena;
        public ItemDropParticle itemDrop;

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
                Enemies = encounter.enemies,
                Items = p.GetConsumables(),
                Callback = OnCombatEnded,
            };

            StartCoroutine(EnterCombat(config));
        }

        private void OnCombatEnded(bool win) => StartCoroutine(ExitCombat(win));

        private IEnumerator EnterCombat(CombatConfig config) {
            ExplorationTask.Add(this);
            Game.Combat.Pause = true;

            yield return FadeStatic(true);

            arena.Camera.SetActive(true);
            Game.Combat.StartCombat(config);
            yield return new WaitForSecondsRealtime(duration.y);

            yield return FadeStatic(false);

            Game.Combat.Pause = false;
            ExplorationTask.Remove(this);
        }

        private IEnumerator ExitCombat(bool win) {
            ExplorationTask.Add(this);

            yield return FadeStatic(true);

            arena.Camera.SetActive(false);
            Game.Combat.gameObject.SetActive(false);
            yield return new WaitForSecondsRealtime(duration.y);

            yield return FadeStatic(false);

            if(win)
                foreach (var enemy in encounter.enemies) {
                    itemDrop.PopCopy(transform.position, enemy.Drops.Random(), 1); //TODO drop chance
                    yield return new WaitForSeconds(.15f);
                }

            ExplorationTask.Remove(this);
        }

        private IEnumerator FadeStatic(bool fadeIn) {
            var material = staticEffect.material;
            staticEffect.gameObject.SetActive(true);

            float start = fadeIn ? 0 : 1;
            float end = fadeIn ? 1 : 0;

            for (var i = 0f; i < 1f; i += Time.unscaledDeltaTime / duration.x) {
                var t = Mathf.Lerp(start, end, i);
                material.SetFloat(AlphaID, t);
                material.SetFloat(PixelsID, pixels.x + pixels.y * t);
                yield return null;
            }

            material.SetFloat(AlphaID, end);
            material.SetFloat(PixelsID, pixels.x + pixels.y * end);

            if(!fadeIn) staticEffect.gameObject.SetActive(false);
        }
    }
}