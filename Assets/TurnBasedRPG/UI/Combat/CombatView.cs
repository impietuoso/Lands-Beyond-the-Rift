using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using UnityEngine;

namespace TurnBasedRPG.UI.Combat {
    public class CombatView : DataView<CombatManager> {
        [Header("UI Components")]
        [field: SerializeField] public CanvasGroup Canvas { get; private set; }
        [field: SerializeField] public ListView AlliesView { get; private set; }
        [field: SerializeField] public ListView EnemiesView { get; private set; }
        [field: SerializeField] public GameObject ActionsPanel { get; private set; }
        [field: SerializeField] public GameObject ListsPanel { get; private set; }
        [field: SerializeField] public ListView SkillsView { get; private set; }
        [field: SerializeField] public IInventoryView ItemsView { get; private set; }
        [field: SerializeField] public InfoPanel Info { get; private set; }
        [field: SerializeField] public GameObject GameOverPanel { get; private set; }
        [field: SerializeField] public CallPopupText CallPopup { get; private set; }

        public IEnumerable<CharacterView> Everyone { get; private set; }

        private SkillArgs _preparation;

        private void Start() {
            Everyone = AlliesView.templateList.Concat(EnemiesView.templateList).OfType<CharacterView>();
        }

        protected override void Subscribe() {
            AlliesView.SetData(Data.Allies);
            EnemiesView.SetData(Data.Enemies);
            ItemsView.SetData(Data.Consumables);

            GameOverPanel.SetActive(false);
            ActionsPanel.SetActive(false);
            ListsPanel.SetActive(false);
            SkillsView.gameObject.SetActive(false);
            ItemsView.gameObject.SetActive(false);
            gameObject.SetActive(true);
        }

        protected override void Unsubscribe() {
            AlliesView.SetData(null);
            EnemiesView.SetData(null);

            gameObject.SetActive(false);
        }

        public void SelectSkill(AvailableSkillView view) => PrepareSkill(view.Data.s, view.Data.c);

        public void ShowActions(Character character) {
            if(character != null)
                SkillsView.SetData(character.skills.Select(s => (character, s)));
            ListsPanel.SetActive(true);
            ActionsPanel.SetActive(true);
            SkillsView.gameObject.SetActive(true);
            ItemsView.gameObject.SetActive(false);
            Info.Hide();
        }
        
        public void HideActions() {
            ListsPanel.SetActive(false);
            ActionsPanel.SetActive(false);
            SkillsView.gameObject.SetActive(false);
            ItemsView.gameObject.SetActive(false);
        }

        private void PrepareSkill(ISkill skill, Character user) {
            if(!skill.Available(user)) return;

            SkillsView.gameObject.SetActive(false);
            Info.Show($"Select Target - {skill.SkillName}: {skill.BriefDesc}");

            _preparation = new(skill, user, null);

            if(skill.Targeting.SkipSelection) {
                SelectTarget(user);
                return;
            }

            var eligible = skill.Targeting.EligibleTargets(user);
            Data.Arena.ClearTargets();
            Data.Arena.TargetCharacters(eligible);
        }

        public void SelectTarget(Character target) {
            if(_preparation == null) return;
            _preparation.Target = target;
            Data.SelectedAction = _preparation;
            _preparation = null;
            Data.Arena.ClearTargets();
            Info.Hide();
        }

        public void CancelPreparation() {
            Info.Hide();
            ShowActions(null);
            _preparation = null;
            Data.Arena.ClearTargets();
        }

        public void PrepareAttackForCurrentPlayer() {
            var currentPlayer = Data.CurrentCharacter;
            PrepareSkill(currentPlayer.basicAttack, currentPlayer);
        }

        public void PrepareDefenseForCurrentPlayer() {
            var currentPlayer = Data.CurrentCharacter;
            PrepareSkill(currentPlayer.member.BasicDefense, currentPlayer);
        }

        public void PrepareSkillForCurrentPlayer(IItemView itemView) {
            var currentPlayer = Data.CurrentCharacter;
            PrepareSkill(((IConsumable)itemView.Data).Skill, currentPlayer);
        }
    }
}