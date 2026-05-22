using System;
using System.Collections;
using TurnBasedRPG.Data;
using UnityEngine;

namespace TurnBasedRPG.Skills.SkillAnimations {
    [Serializable, Obsolete]
    public class SelfHit : ISkillAnimation {
        public float damageDelay = 1;
        public GameObject castingParticle;
        public GameObject skillParticle;

        public IEnumerator Play(Skill skill, Character user, Character target, CombatManager cm) {
            var ui = CombatManager.instance.view;
            if (castingParticle) {
                var particle = UnityEngine.Object.Instantiate(
                    castingParticle,
                    ui.GetCharacterWorldPosition(user),
                    Quaternion.identity);
                yield return new WaitWhile(() => particle);
            } else
                yield return new WaitForSeconds(0.1f);

            Debug.Log(user.characterName + " Defends!");
            var args = new CombatArgs();
            args.source = this;
            args.skill = skill;
            args.element = skill.element;
            args.user = user;
            args.target = user;

            foreach (var effect in skill.skillEffects)
                effect.Prepare(args);

            UnityEngine.Object.Instantiate(skillParticle, ui.GetCharacterWorldPosition(args.target), Quaternion.identity);
            yield return new WaitForSeconds(damageDelay);

            args.Resolve();
        }
    }
}
