using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills;
using UnityEngine;
using UnityEngine.Serialization;

namespace RPG {
    [CreateAssetMenu(menuName = "Scriptable/Skill", fileName = "New Skill")]
    public class Skill : ScriptableObject, ISkill {
        [SerializeField] private string skillName;
        [SerializeField, TextArea(3, 6)] private string skillDescription;
        [SerializeField] private int cost;
        [SerializeField] private Sprite icon;
        [SerializeField] private Element element;

        [SerializeField, Separator] private Targeting targeting;
        [SerializeReference, TypeInstance, Separator] public ISkillAnimationOld animation;

        [FormerlySerializedAs("skillEffects")]
        [Separator, SerializeReference, TypeInstance] public List<ISkillEffectOld> effects;

        public string SkillName => skillName;
        public string Description => skillDescription;
        public int Cost => cost;
        public Sprite Icon => icon;
        public Element Element => element;
        public Targeting Targeting => targeting;
        public ISkillAnimationOld Animation => animation;
        public List<ISkillEffectOld> Effects => effects;

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