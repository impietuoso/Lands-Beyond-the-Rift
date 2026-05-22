using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG.Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace TurnBasedRPG.Skills.SkillAnimations {
    [Serializable]
    public class MultiHit : ISkillAnimation {
        public GameObject castParticle;
        public GameObject skillParticle;
        public Vector2Int hitCount = new(1, 1);
        public float damageDelay = 1;
        public float hitDelay = 1;

        public IEnumerator Play(Skill skill, Character user, Character target, CombatManager cm) {
            throw new InvalidOperationException();
        }

        public IEnumerator Play(SkillArgs cast) {
            var ui = CombatManager.instance.combatUI;
            if(castParticle) {
                var pos = ui.GetCharacterWorldPosition(cast.User);
                var particle = Object.Instantiate(castParticle, pos, Quaternion.identity);
                yield return new WaitWhile(() => particle);
            }
            else
                yield return new WaitForSeconds(0.1f);

            var newHitCount = UnityEngine.Random.Range(hitCount.x, hitCount.y + 1);
            var routines = new List<Coroutine>();

            foreach (var newTarget in cast.Skill.targeting.GetAffectedTargets(cast.User, cast.Target)) {
                var ie = SingleTargetDamage(cast.Skill, cast.User, newTarget, newHitCount);
                routines.Add(cast.Cm.StartCoroutine(ie));
            }

            foreach (var exe in routines)
                yield return exe;

            if(hitCount.y > 1) Debug.Log(newHitCount + " Hits");
        }

        public IEnumerator SingleTargetDamage(Skill skill, Character user, Character target, int newHitCount) {
            for (var i = 0; i < newHitCount; i++) {
                var args = new CombatArgs
                {
                    source = this,
                    skill = skill,
                    element = skill.element,
                    target = target,
                    user = user,
                };

                foreach (var effect in skill.skillEffects)
                    effect.Prepare(args);

                var ui = CombatManager.instance.combatUI;
                if(skillParticle) {
                    var pos = ui.GetCharacterWorldPosition(args.target);
                    Object.Instantiate(skillParticle, pos, Quaternion.identity);
                }
                else
                    Debug.LogError("No Particle, add it to: " + skill.skillName);

                yield return new WaitForSeconds(damageDelay);
                args.Resolve();
                yield return new WaitForSeconds(hitDelay);
            }

            yield return new WaitForSeconds(1.2f);
        }
    }
}