using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.TargetFilters;
using UnityEngine;


[Serializable, Obsolete]
public class MultiHitAnimate : ISkillAnimationOld {
    public bool singleTarget;
    public bool targetEnemy;
    public bool targetDead;
    public Vector2Int hitCount = new(1, 1);
    public float damageDelay = 1;
    public float hitDelay = 1;
    public GameObject castingParticle;
    public GameObject skillParticle;
    public bool TrySkipSelection(Character user, ISkill skill) => false;

    public Targeting GetTargeting(ISkill skill) {
        return new Targeting
        {
            group = targetEnemy ? TargetGroup.Enemy : TargetGroup.Ally,
            area = singleTarget ? TargetArea.One : TargetArea.All,
            filter = targetDead ? new Dead() : new Alive()
        };
    }

    public IEnumerable<Character> GetAffectedTargets(Character user, Character target) {
        if(singleTarget) {
            yield return target;
        }
        else {
            foreach (var newTarget in target.cm.Everyone) {
                if(ValidateTarget(user, newTarget)) yield return newTarget;
            }
        }
    }

    public IEnumerator Play(ISkill skill, Character user, Character target, CombatManager cm) {
        if(castingParticle) {
            var particle = castingParticle.Clone(user.Position);
            yield return new WaitWhile(() => particle);
        }
        else
            yield return new WaitForSeconds(0.1f);

        var newHitCount = UnityEngine.Random.Range(hitCount.x, hitCount.y + 1);
        var routines = new List<Coroutine>();

        foreach (var newTarget in GetAffectedTargets(user, target))
            routines.Add(cm.StartCoroutine(SingleTargetDamage(skill, user, newTarget, newHitCount)));

        foreach (var exe in routines)
            yield return exe;

        if(hitCount.y > 1) Debug.Log(newHitCount + " Hits");
    }

    public IEnumerator SingleTargetDamage(ISkill skill, Character user, Character target, int newHitCount) {
        for (var i = 0; i < newHitCount; i++) {
            var args = new CombatArgs();
            args.skill = skill;
            args.element = skill.Element;
            args.target = target;
            args.user = user;
            args.source = this;

            foreach (var effect in skill.Effects) {
                effect.Prepare(args);
            }

            if(skillParticle)
                skillParticle.Clone(args.target.Position);
            else
                Debug.Log("No Particle, add it to: " + skill.SkillName);

            yield return new WaitForSeconds(damageDelay);
            args.Resolve();
            yield return new WaitForSeconds(hitDelay);
        }

        yield return new WaitForSeconds(1.2f);
    }

    public bool ValidateTarget(Character user, Character target) {
        var sameTeam = user.team == target.team;
        var alive = target.Health.Current > 0;
        return sameTeam ^ targetEnemy && alive ^ targetDead;
    }
}