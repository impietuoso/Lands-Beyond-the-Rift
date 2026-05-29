using System;
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
        [field: SerializeField] public CombatView View { get; private set; }

        [field: Header("Runtime")]
        [field: SerializeField] public Character CurrentCharacter { get; set; }
        [field: SerializeField] public List<Character> Allies { get; private set; } = new();
        [field: SerializeField] public List<Character> Enemies { get; private set; } = new();
        [field: SerializeField] public ListInventory<IConsumable> Consumables { get; private set; }
        [field: SerializeField] public int MaxSpeed { get; set; }
        [field: SerializeField] public int TurnCount { get; set; }
        [field: SerializeField] public bool Pause { get; set; }
        [field: SerializeField] public bool CombatWon { get; set; }

        public CombatConfig Config { get; set; }
        public CombatArena Arena => Config.Arena;
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
            _endPhases.Add(new ResultPhase());
        }

        public void StartCombat(CombatConfig config) {
            if(Config != null) throw new Exception("Last combat not terminated");
            Config = config;
            gameObject.SetActive(true);

            Allies.Clear();
            Enemies.Clear();
            Everyone = Allies.Concat(Enemies);
            Consumables = new();

            foreach (var member in config.Allies)
                Allies.Add(new(this, member, "Player", Allies, Enemies));

            foreach (var member in config.Enemies)
                Enemies.Add(new(this, member, "Enemy", Enemies, Allies));

            foreach (var (item, amt) in config.Items) {
                if(item == null) continue;
                Consumables.Add(item, amt);
            }

            foreach (var character in Everyone)
                character.SubscribePassives();

            _currentPhase = StartCoroutine(CombatLoop());
            View.SetData(this);
            Arena.SetData(this);
        }

        public void FinishCombat(bool won) {
            CombatWon = won;
            StopCoroutine(_currentPhase);
            _currentPhase = StartCoroutine(CombatEnd());
        }

        private IEnumerator CombatLoop() {
            foreach (var phase in _setupPhases)
                yield return phase.Execute(this);

            while (true)
                foreach (var phase in _loopPhases)
                    yield return phase.Execute(this);
            // ReSharper disable once IteratorNeverReturns
        }

        private IEnumerator CombatEnd() {
            foreach (var phase in _endPhases)
                yield return phase.Execute(this);
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