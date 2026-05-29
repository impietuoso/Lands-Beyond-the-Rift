using System.Collections.Generic;
using System.Linq;
using TMPro;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.UI.Views;
using UnityEngine;

namespace TurnBasedRPG.UI.Combat {
    public class CombatView : DataView<CombatManager> {
        [Header("UI Components")]
        public ListView alliesView;
        public ListView enemiesView;
        public ListView skillsView;
        public IInventoryView consumablesView;

        public CanvasGroup canvas;
        public GameObject combatPanel;
        public GameObject actionsPanel;
        public GameObject skillPanel;
        public GameObject itemsPanel;
        public GameObject selectTargetPanel;
        public GameObject currentActionPanel;
        public GameObject gameOverPanel;

        public TMP_Text currentActionText;
        public TMP_Text currentSkillNameText;
        public TMP_Text currentSkillDescriptionText;

        public IEnumerable<CharacterView> Everyone { get; private set; }

        public CallPopupText callPopup;
        public StatusEffectListView statusEffectsDescription;
        private SkillArgs _preparation;

        private void Start() {
            Everyone = alliesView.templateList.Concat(enemiesView.templateList).OfType<CharacterView>();
        }

        protected override void Subscribe() {
            alliesView.SetData(Data.Allies);
            enemiesView.SetData(Data.Enemies);
            consumablesView.SetData(Data.Consumables);
            
            currentActionPanel.SetActive(false);
            actionsPanel.SetActive(false);
            combatPanel.SetActive(false);
            skillPanel.SetActive(false);
            itemsPanel.SetActive(false);
            gameObject.SetActive(true);
        }

        protected override void Unsubscribe() {
            alliesView.SetData(null);
            enemiesView.SetData(null);
            
            gameObject.SetActive(false);
        }

        public void ShowSkills(Character character) => skillsView.SetData(character.skills.Select(s => (character, s)));
        public void SelectSkill(AvailableSkillView view) => PrepareSkill(view.Data.s, view.Data.c);

        private void PrepareSkill(ISkill skill, Character user) {
            if(!skill.Available(user)) return;

            skillPanel.SetActive(false);
            selectTargetPanel.SetActive(true);
            currentSkillNameText.text = "[" + skill.SkillName + "]";
            currentSkillDescriptionText.text = skill.Description;

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
        }
        
        public void CancelPreparation() {
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

        public void ShowCurrentStatusEffects(StatusEffectListView statusEffectListView) {
            statusEffectsDescription.SetData(statusEffectListView.owner);
        }

        public void ShowCurrentAction(string userName, string actionName) {
            if(currentActionPanel.activeInHierarchy)
                currentActionPanel.SetActive(false);
            currentActionPanel.SetActive(true);
            currentActionText.text = userName + " uses " + actionName;
        }
    }
}