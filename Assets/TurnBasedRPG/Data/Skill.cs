using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.Data;
using UnityEngine;

namespace TurnBasedRPG.Skill
{
    [CreateAssetMenu(menuName = "Scriptable/Skill", fileName = "New Skill")]
    public class Skill : ScriptableObject {
        [Header("Ui")]
        public string skillName;
        [TextArea(3, 6)]
        public string skillDescription;
        public int cost;
        public Sprite icon;
        [Header("Config")]
        public Element element;
        [SerializeReference, TypeDropdown(typeof(ISkillAnimation))]
        public ISkillAnimation animation;
        [SerializeReference, Effect]
        public ISkillEffect[] skillEffects;
        [SerializeReference, TypeDropdown(typeof(IPassiveSkill))] public IPassiveSkill passiva;
    
        public bool Available(Character user) {
            //TODO checksilence
            // foreach (var status in user.StatusEffectList.StatusList) {
            //     if (status.Value is Silence) return false;
            // }
        
            if (user.Mana.Current < cost) return false;

            if (CombatManager.instance.combatUI.characters.All
                    (c => !animation.ValidateTarget(user, c.owner))) return false;

            return true;
        }
    
        public virtual IEnumerator UseSkill(Character user, Character target, CombatManager combatManager) {
            Debug.Log(user.characterName + " used " + skillName);
            return animation.Play(this, user, target, combatManager);
        }
    }
    
    public interface ISkillAnimation {
        public bool TrySkipSelection(Character user, Skill skill);
        public bool ValidateTarget(Character user, Character target);
        public IEnumerable<Character> GetAffectedTargets(Character user, Character target);
        public IEnumerator Play(Skill skill, Character user, Character target, CombatManager cm);
    }
    
    public interface ISkillEffect {
        public void Prepare(CombatArgs args);
    }
}