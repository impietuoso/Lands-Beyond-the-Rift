using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG.Data;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TurnBasedRPG.Skills.SkillAnimations {
    [Serializable, Obsolete]
    public class RandomHit : ISkillAnimation {
        public bool targetEnemy;
        public bool targetDead;
        public Vector2Int hitCount = new(1, 1);
        public float damageDelay = 1;
        public float hitDelay = 1;
        public GameObject castingParticle;
        public GameObject skillParticle;
        public bool TrySkipSelection(Character user, ISkill skill) => false;

        public IEnumerator Play(ISkill skill, Character user, Character target, CombatManager cm) {
            if(castingParticle) {
                var particle = castingParticle.Clone(user.Position);
                yield return new WaitWhile(() => particle);
            }
            else
                yield return new WaitForSeconds(0.1f);

            var targets = new List<Character>(skill.Targeting.GetAffectedTargets(user, target));
            var newHitCount = Random.Range(hitCount.x, hitCount.y + 1);
            yield return cm.StartCoroutine(SingleTargetDamage(skill, user, targets, newHitCount));

            if(hitCount.y > 1) Debug.Log(newHitCount + " Hits");
        }

        public IEnumerator SingleTargetDamage(ISkill skill, Character user, List<Character> targets, int newHitCount) {
            for (var i = 0; i < newHitCount; i++) {
                var args = new CombatArgs
                {
                    skill = skill,
                    element = skill.Element,
                    target = targets[Random.Range(0, targets.Count)],
                    user = user,
                    source = this
                };

                foreach (var effect in skill.Effects) {
                    effect.Prepare(args);
                }

                skillParticle.Clone(args.target.Position);
                yield return new WaitForSeconds(damageDelay);

                args.Resolve();
                yield return new WaitForSeconds(hitDelay);
            }
        }

        public bool ValidateTarget(Character user, Character target) {
            var sameTeam = user.team == target.team;
            var alive = target.Health.Current > 0;
            return sameTeam ^ targetEnemy && alive ^ targetDead;
        }
    }
}