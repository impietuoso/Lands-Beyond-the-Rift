using UnityEngine;

namespace TurnBasedRPG.Controller {
    public class EncounterTrigger : MonoBehaviour {
        public EnemyEncounter encounter;
        public GameObject arena;

        private void OnTriggerEnter2D(Collider2D other) {
            if(!other.TryGetComponent(out PlayerParty p)) return;

            var config = new CombatConfig
            {
                Allies = p.members,
                Enemies = encounter.enemyList,
                Items = p.consumables.slots,
            };

            arena.SetActive(true);
            CombatManager.instance.StartCombat(config);
        }
    }
}