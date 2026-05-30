using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills;
using UnityEngine;

namespace Data {
    [CreateAssetMenu(menuName = "Scriptable/Item/Consumable", fileName = "New Consumable")]
    public class Consumable : Item, IConsumable, ISkill {
        [Separator]
        [SerializeField] private Element element;
        [SerializeField] private Targeting targeting;
        [SerializeReference, TypeInstance] private ISkillAnimationOld animation;
        [SerializeReference, TypeInstance] private List<ISkillEffectOld> effects;

        public ISkill Skill => this;
        public string SkillName => displayName;
        public string Description => description;
        public string BriefDesc => description;
        public override int maxStack => 1;
        public int Cost => 0;
        public Sprite Icon => sprite;
        public Element Element => element;
        public Targeting Targeting => targeting;
        public ISkillAnimationOld Animation => animation;
        public List<ISkillEffectOld> Effects => effects;

        public bool Available(Character user) {
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