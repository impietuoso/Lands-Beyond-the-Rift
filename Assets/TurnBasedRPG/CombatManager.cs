using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.UI.Combat;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TurnBasedRPG {
    public class CombatManager : MonoBehaviour {
        [field: SerializeField] public Skill BasicDefense { get; private set; }
        [field: SerializeField] public CombatView View { get; private set; }
        [field: SerializeField] public CombatArena Models { get; private set; }

        [Header("Runtime")]
        [field: SerializeField] public List<Character> Allies { get; private set; } = new();
        [field: SerializeField] public List<Character> Enemies { get; private set; } = new();
        [field: SerializeField] public ListInventory<Consumable> Consumables { get; private set; }
        [field: SerializeField] public Character CurrentCharacter { get; set; }
        [field: SerializeField] public int MaxSpeed { get; set; }
        [field: SerializeField] public int TurnCount { get; set; }
        [field: SerializeField] public bool Pause { get; set; }
        [field: SerializeField] public bool CombatWon { get; set; }

        public IEnumerable<Character> Everyone { get; private set; }
        public readonly Queue<IEnumerator> CombatEvents = new();
        public readonly HashSet<object> ActionFlags = new();
        public SkillArgs WaitFlag { get; } = new();
        public SkillArgs SelectedAction;

        private Coroutine _currentPhase;
        private readonly List<ICombatPhase> _setupPhases = new();
        private readonly List<ICombatPhase> _loopPhases = new();
        private readonly List<ICombatPhase> _endPhases = new();

        private void Awake() {
            _setupPhases.Add(new SetupPhase());
            _loopPhases.Add(new WaitActionPhase());
            _loopPhases.Add(new CharacterPhase(new EnemyBehaviour()));
            _loopPhases.Add(new CombatEvents());
            _loopPhases.Add(new CheckResultPhase());
        }

        public void StartCombat(CombatConfig config) {
            config.Arena.Camera.SetActive(true);

            Allies.Clear();
            Enemies.Clear();
            Everyone = Allies.Concat(Enemies);
            Consumables = new();

            foreach (var member in config.Allies) {
                if(!member) continue;
                Allies.Add(new(this, member, "Player", Allies, Enemies));
            }

            foreach (var member in config.Enemies) {
                if(!member) continue;
                Enemies.Add(new(this, member, "Enemy", Enemies, Allies));
            }

            foreach (var slot in config.Items) {
                if(!slot.item) continue;
                Consumables.Add(slot.item, slot.amount);
            }

            foreach (var character in Everyone)
                character.SubscribePassives();

            _currentPhase = StartCoroutine(CombatLoop());
            View.SetData(this);
            Models.SetData(this);
        }

        public void FinishCombat(bool won) {
            CombatWon = won;
            StopCoroutine(_currentPhase);
            StartCoroutine(CombatEnd());
        }

        public IEnumerator CombatLoop() {
            foreach (var phase in _setupPhases) {
                yield return phase.Execute(this);
            }

            while (true) {
                foreach (var phase in _loopPhases) {
                    yield return phase.Execute(this);
                }
            }
        }

        public IEnumerator CombatEnd() {
            foreach (var phase in _endPhases) {
                yield return phase.Execute(this);
            }
        }

        [ContextMenu("Skip Turn ( ͡° ͜ʖ ͡°)")]
        public void SkipTurn() {
            SelectedAction = null;
            View.combatPanel.SetActive(false);
            View.actionsPanel.SetActive(false);
        }

        public void ReloadScene() {
            SceneManager.LoadSceneAsync(0);
        }
    }
}