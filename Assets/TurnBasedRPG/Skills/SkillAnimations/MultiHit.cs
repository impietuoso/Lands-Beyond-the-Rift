using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG.Data;
using UnityEngine;
using UnityEngine.Scripting;
using Object = UnityEngine.Object;

namespace TurnBasedRPG.Skills.SkillAnimations {
    [Preserve, Serializable]
    public class MultiHit : ISkillAnimation {
        public GameObject castParticle;
        public GameObject skillParticle;
        public Vector2Int hitCount = new(1, 1);
        public float damageDelay = 1;
        public float hitDelay = 1;

        public IEnumerator Play(SkillArgs cast) {
            if(castParticle) {
                var pos = cast.User.Position;
                var particle = Object.Instantiate(castParticle, pos, Quaternion.identity);
                yield return new WaitWhile(() => particle);
            }
            else
                yield return new WaitForSeconds(0.1f);

            // cast.Cm.View.canvas.alpha = 0;
            // cast.Cm.Arena.ActionCamera.SetActive(true);
            cast.User.PlayAnimation?.Invoke("Attack", 0);

            var hits = UnityEngine.Random.Range(hitCount.x, hitCount.y + 1);
            var routines = new List<Coroutine>();

            foreach (var tgt in cast.Targets) {
                var ie = Damage(cast.Skill, cast.User, tgt, hits);
                routines.Add(cast.Cm.StartCoroutine(ie));
            }

            foreach (var exe in routines)
                yield return exe;

            // cast.Cm.Arena.ActionCamera.SetActive(false);
            // yield return new WaitForSeconds(.25f);
            // cast.Cm.View.canvas.alpha = 1;
            
            if(hitCount.y > 1) Debug.Log(hits + " Hits");
        }

        private IEnumerator Damage(ISkill skill, Character user, Character target, int hits) {
            for (var i = 0; i < hits; i++) {
                var args = new CombatArgs
                {
                    source = this,
                    skill = skill,
                    element = skill.Element,
                    target = target,
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