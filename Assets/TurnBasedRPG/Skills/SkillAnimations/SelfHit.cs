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

        public IEnumerator Play(ISkill skill, Character user, Character target, CombatManager cm) {
            if(castingParticle) {
                var particle = castingParticle.Clone(user.Position);
                yield return new WaitWhile(() => particle);
            }
            else
                yield return new WaitForSeconds(0.1f);

            Debug.Log(user.characterName + " Defends!");
            var args = new CombatArgs
            {
                source = this,
                skill = skill,
                element = skill.Element,
                user = user,
                target = user
            };

            foreach (var effect in skill.Effects)
                effect.Prepare(args);

            skillParticle.Clone(args.target.Position);
            yield return new WaitForSeconds(damageDelay);

            args.Resolve();
        }
    }
}