using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills;
using UnityEngine;
using UnityEngine.Serialization;

namespace Data {
    [CreateAssetMenu(menuName = "Scriptable/Skill", fileName = "New Skill")]
    public class Skill : Item, ISkill {
        [Separator]
        [SerializeField] private int cost;
        [SerializeField] private Element element;

        [SerializeField, Separator] private Targeting targeting;
        [SerializeReference, TypeInstance, Separator] public ISkillAnimationOld animation;

        [FormerlySerializedAs("skillEffects")]
        [Separator, SerializeReference, TypeInstance] public List<ISkillEffectOld> effects;

        public string SkillName => displayName;
        public string Description => description;
        public override int maxStack => 1;
        public int Cost => cost;
        public Sprite Icon => sprite;
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
            Debug.Log($"{user?.characterName} used {displayName} on {target.characterName}");
            var cast = new SkillArgs(this, user, target);
            return animation.Play(cast);
        }
    }
}