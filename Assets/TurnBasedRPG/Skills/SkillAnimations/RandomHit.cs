using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG.Data;
using UnityEngine;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace TurnBasedRPG.Skills.SkillAnimations {
    [Preserve, Serializable]
    public class RandomHit : ISkillAnimation {
        public GameObject castParticle;
        public GameObject skillParticle;
        public Vector2Int hitCount = new(1, 1);
        public float damageDelay = 1;
        public float hitDelay = 1;

        public IEnumerator Play(SkillArgs cast) {
            if(castParticle) {
                var particle = Object.Instantiate(castParticle, cast.User.Position, Quaternion.identity);
                yield return new WaitWhile(() => particle);
            }
            else
                yield return new WaitForSeconds(0.1f);

            var hits = Random.Range(hitCount.x, hitCount.y + 1);
            var targets = new List<Character>(cast.Targets);
            yield return RandomDamage(cast.Skill, cast.User, targets, hits);
            if(hitCount.y > 1) Debug.Log(hits + " Hits");
        }

        public IEnumerator RandomDamage(ISkill skill, Character user, List<Character> targets, int newHitCount) {
            for (var i = 0; i < newHitCount; i++) {
                var args = new CombatArgs
                {
                    source = this,
                    skill = skill,
                    element = skill.Element,
                    target = targets[Random.Range(0, targets.Count)],
                    user = user,
                };

                foreach (var effect in skill.Effects)
                    effect.Prepare(args);

                if(skillParticle)
                    Object.Instantiate(skillParticle, args.target.Position, Quaternion.identity);
                else
                    Debug.LogError("No Particle, add it to: " + skill.SkillName);

                yield return new WaitForSeconds(damageDelay);
                args.Resolve();
                yield return new WaitForSeconds(hitDelay);
            }

            yield return new WaitForSeconds(1.2f);
        }
    }
}