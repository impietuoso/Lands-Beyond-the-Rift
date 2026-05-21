using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills;
using UnityEngine;

namespace TurnBasedRPG.Data {
    [CreateAssetMenu(menuName = "Scriptable/Skill", fileName = "New Skill")]
    public class Skill : ScriptableObject {
        [Header("Ui")] public string skillName;
        [TextArea(3, 6)] public string skillDescription;
        public int cost;
        public Sprite icon;

        [Header("Config")]
        public Element element;
        [SerializeReference, TypeInstance] public ISkillAnimation animation;
        [SerializeReference, TypeInstance] public List<ISkillEffect> skillEffects;
        [SerializeReference, TypeInstance] public ISkillEffect effect;
        [SerializeReference, TypeInstance] public IPassiveSkill passiva;

        public bool Available(Character user) {
            //TODO checksilence
            // foreach (var status in user.StatusEffectList.StatusList) {
            //     if (status.Value is Silence) return false;
            // }

            if(user.Mana.Current < cost) return false;

            if(CombatManager.instance.combatUI.characters.All
                   (c => !animation.ValidateTarget(user, c.owner))) return false;

            return true;
        }

        public virtual IEnumerator UseSkill(Character user, Character target, CombatManager combatManager) {
            Debug.Log(user.characterName + " used " + skillName);
            return animation.Play(this, user, target, combatManager);
        }
    }
}