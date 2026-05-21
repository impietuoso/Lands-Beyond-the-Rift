using System.Collections.Generic;
using System.Linq;
using TMPro;
using TurnBasedRPG.Data;
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

        public StatusEffectListView statusEffectsDescription;
        public CallPopupText callPopup;
        public Camera cam;

        private void Start() {
            cam = Camera.main;
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

        public void ShowSkills(Character character) {
            skillsView.SetData(character.skills.Select(s => (character, s)));
        }

        private void SelectSkill(AvailableSkillView view) {
            if(!view.Data.s.Available(view.Data.c)) return;
            Data.selectedSkill = view.Data.s;
        }

        private void PrepareSkill(Character user, Skill skill) {
            if(user.Mana.Current >= skill.cost) {
                skillPanel.SetActive(false);
                selectTargetPanel.SetActive(true);
                currentSkillNameText.text = "[" + skill.skillName + "]";
                currentSkillDescriptionText.text = skill.skillDescription;
                if(skill.animation.TrySkipSelection(user, skill)) {
                    CombatManager.instance.UsingSkillOnTarget(user, skill, user);
                }
                else VerifyTargets(user, skill);
            }
            else {
                Debug.Log("Don't have enough Mana");
            }
        }

        public void PrepareAttackForCurrentPlayer() {
            var currentPlayer = CombatManager.instance.currentCharacter;
            PrepareSkill(currentPlayer, currentPlayer.basicAttack);
        }

        public void PrepareDefenseForCurrentPlayer() {
            var currentPlayer = CombatManager.instance.currentCharacter;
            PrepareSkill(currentPlayer, CombatManager.instance.basicDefense);
        }

        public void PrepareSkillForCurrentPlayer(IItemView itemView) {
            var currentPlayer = CombatManager.instance.currentCharacter;
            PrepareSkill(currentPlayer, ((Consumable)itemView.Data).skillEffect);
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