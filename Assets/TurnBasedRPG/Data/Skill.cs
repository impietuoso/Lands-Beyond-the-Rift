using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills;
using UnityEngine;

namespace TurnBasedRPG.Data {
    [CreateAssetMenu(menuName = "Scriptable/Skill", fileName = "New Skill")]
    public class Skill : ScriptableObject {
        public string skillName;
        [TextArea(3, 6)] public string skillDescription;
        public int cost;
        public Sprite icon;
        public Element element;

        [SerializeReference, TypeInstance, Separator] public Targeting targeting;
        [SerializeReference, TypeInstance, Separator] public ISkillAnimationOld animation;
        [Separator, SerializeReference, TypeInstance] public List<ISkillEffectOld> skillEffects;

        public bool Available(Character user) {
            if(user.Silence) return false;
            if(user.Mana.Current < cost) return false;
            var hasTargets = targeting.EligibleTargets(user).Any();
            return hasTargets;
        }

        public IEnumerator UseSkill(Character user, Character target) {
            Debug.Log($"{user?.characterName} used {skillName} on {target.characterName}");
            var cast = new SkillArgs(this, user, target);
            return animation.Play(cast);
        }
    }
}