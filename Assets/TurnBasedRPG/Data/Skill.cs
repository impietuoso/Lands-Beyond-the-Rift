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

        [Header("Config")]
        public Element element;

        [SerializeReference, TypeInstance, Separator] public Targeting targeting;
        [SerializeReference, TypeInstance, Separator] public ISkillAnimation animation;
        [Separator, SerializeReference, TypeInstance] public List<ISkillEffectOld> skillEffects;

        public bool Available(Character user) {
            //TODO checksilence
            // foreach (var status in user.StatusEffectList.StatusList) {
            //     if (status.Value is Silence) return false;
            // }

            if(user.Mana.Current < cost) return false;
            var hasTargets = targeting.EligibleTargets(user).Any();
            return hasTargets;
        }

        public IEnumerator UseSkill(SkillArgs cast) {
            Debug.Log(user.characterName + " used " + skillName);
            return animation.Play(this, user, target, target.cm);
        }
    }
}