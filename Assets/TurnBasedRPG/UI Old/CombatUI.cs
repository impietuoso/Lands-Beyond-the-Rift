using System.Collections.Generic;
using System.Linq;
using TMPro;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.UI.Views;
using UnityEngine;

namespace TurnBasedRPG.UI {
    public class CombatUI : DataView<CombatManager> {
        [Header("UI Components")]
        public ListView alliesView;
        public ListView enemiesView;
        public ListView skillsView;
        public IInventoryView consumablesView;

        public GameObject combatPanel;
        public GameObject actionsPanel;
        public GameObject skillPanel;
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
            alliesView.SetData(Data.allies);
            enemiesView.SetData(Data.enemies);
            consumablesView.SetData(Data.consumables);

            combatPanel.SetActive(true);
        }

        protected override void Unsubscribe() {
            alliesView.SetData(null);
            enemiesView.SetData(null);

            combatPanel.SetActive(false);
        }

        public void ShowSkills(Character character) => skillsView.SetData(character.skills.Select(s => (character, s)));
        public void SelectSkill(AvailableSkillView view) => PrepareSkill(view.Data.s, view.Data.c);

        private void PrepareSkill(Skill skill, Character user) {
            if(!skill.Available(user)) return;

            skillPanel.SetActive(false);
            selectTargetPanel.SetActive(true);
            currentSkillNameText.text = "[" + skill.skillName + "]";
            currentSkillDescriptionText.text = skill.skillDescription;

            _preparation = new(skill, user, null);
            
            if(skill.targeting.SkipSelection) {
                SelectTarget(user);
                return;
            }

            var eligible = skill.targeting.EligibleTargets(user);
            Data.models.ClearTargets();
            Data.models.TargetCharacters(eligible);
        }

        public void SelectTarget(Character target) {
            if(_preparation == null) return;
            _preparation.Target = target;
            Data.selectedAction = _preparation;
            _preparation = null;
            Data.models.ClearTargets();
        }
        
        public void CancelPreparation() {
            _preparation = null;
            Data.models.ClearTargets();
        }

        public void PrepareAttackForCurrentPlayer() {
            var currentPlayer = Data.currentCharacter;
            PrepareSkill(currentPlayer.basicAttack, currentPlayer);
        }

        public void PrepareDefenseForCurrentPlayer() {
            var currentPlayer = Data.currentCharacter;
            PrepareSkill(Data.basicDefense, currentPlayer);
        }

        public void PrepareSkillForCurrentPlayer(IItemView itemView) {
            var currentPlayer = Data.currentCharacter;
            PrepareSkill(((Consumable)itemView.Data).skillEffect, currentPlayer);
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

        public Vector3 GetCharacterWorldPosition(Character user) {
            throw new System.NotImplementedException();
        }
    }
}