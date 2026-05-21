using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TurnBasedRPG {
    public class CombatConfig {
        public IEnumerable<PartyMember> Allies;
        public IEnumerable<PartyMember> Enemies;
        public List<Slot<Item>> Items;
    }

    public class CombatManager : MonoBehaviour {
        public static CombatManager instance;

        public Skill basicDefense;
        public CombatUI combatUI;
        public CombatModelManager models;

        [Header("Runtime")]
        public List<Character> allies { get; } = new();
        public List<Character> enemies { get; } = new();
        public IEnumerable<Character> everyone { get; private set; }
        public ListInventory<Consumable> consumables { get; private set; }
        public int maxSpeed;
        public int turnCount;

        public Character currentCharacter { get; set; }

        [Header("Combat Phases")]
        public readonly EnemyBehaviour enemyBehaviour = new();
        public Queue<IEnumerator> combatEvents = new();
        public readonly HashSet<object> actionFlags = new();
        private readonly List<ICombatPhase> setupPhases = new();
        private readonly List<ICombatPhase> loopPhases = new();
        private readonly List<ICombatPhase> endPhases = new();

        public Coroutine currentPhase { get; private set; }

        [Header("Debug Variables")]
        public SkillArgs selectedAction;
        public bool combatWon { get; set; }
        public SkillArgs WaitFlag { get; } = new();

        private void Awake() {
            instance = this;
            setupPhases.Add(new SetupPhase());
            loopPhases.Add(new WaitActionPhase());
            loopPhases.Add(new CharacterPhase());
            loopPhases.Add(new CombatEvents());
            loopPhases.Add(new CheckResultPhase());
        }

        public void StartCombat(CombatConfig config) {
            allies.Clear();
            enemies.Clear();
            everyone = allies.Concat(enemies);
            consumables = new();

            foreach (var member in config.Allies) {
                if(!member) continue;
                allies.Add(new(this, member, "Player", allies, enemies));
            }

            foreach (var member in config.Enemies) {
                if(!member) continue;
                enemies.Add(new(this, member, "Enemy", enemies, allies));
            }

            foreach (var slot in config.Items) {
                if(!slot.item) continue;
                consumables.Add(slot.item, slot.amount);
            }

            foreach (var character in everyone)
                character.SubscribePassives();

            currentPhase = StartCoroutine(CombatLoop());
            combatUI.SetData(this);
            models.SetData(this);
        }

        public void FinishCombat(bool won) {
            combatWon = won;
            StopCoroutine(currentPhase);
            StartCoroutine(CombatEnd());
        }

        public IEnumerator CombatLoop() {
            foreach (var phase in setupPhases) {
                yield return phase.Execute(this);
            }

            while (true) {
                foreach (var phase in loopPhases) {
                    yield return phase.Execute(this);
                }
            }
        }

        public IEnumerator CombatEnd() {
            foreach (var phase in endPhases) {
                yield return phase.Execute(this);
            }
        }

        [ContextMenu("Skip Turn ( ͡° ͜ʖ ͡°)")]
        public void SkipTurn() {
            selectedAction = null;
            combatUI.actionsPanel.SetActive(false);
        }

        public void ReloadScene() {
            SceneManager.LoadSceneAsync(0);
        }
    }
}