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
    public bool TrySkipSelection(Character user, Skill skill) => false;

    public Targeting GetTargeting(Skill skill) {
        return new Targeting
        {
            group = targetEnemy ? TargetGroup.Enemy : TargetGroup.Ally,
            area = singleTarget ? TargetArea.One : TargetArea.All,
            filter = targetDead? new Dead(): new Alive()
        };
    }

    public IEnumerable<Character> GetAffectedTargets(Character user, Character target) {
        if (singleTarget) {
            yield return target;
        } else {
            foreach (var newTarget in target.cm.everyone) {
                if (ValidateTarget(user, newTarget)) yield return newTarget;
            }
        }
    }

    public IEnumerator Play(Skill skill, Character user, Character target, CombatManager cm) {
        var ui = CombatManager.instance.view;
        if (castingParticle) {
            var particle = UnityEngine.Object.Instantiate(
                castingParticle,
                ui.GetCharacterWorldPosition(user),
                Quaternion.identity);
            yield return new WaitWhile(() => particle);
        } else {
            yield return new WaitForSeconds(0.1f);
        }

        int newHitCount = UnityEngine.Random.Range(hitCount.x, hitCount.y + 1);

        List<Coroutine> executores = new();

        foreach (var newTarget in GetAffectedTargets(user, target)) {
            executores.Add(cm.StartCoroutine(SingleTargetDamage(skill, user, newTarget, newHitCount)));
        }

        foreach (var exe in executores) {
            yield return exe;
        }

        if (hitCount.y > 1) Debug.Log(newHitCount + " Hits");
    }

    public IEnumerator SingleTargetDamage(Skill skill, Character user, Character target, int newHitCount) {
        for (int i = 0; i < newHitCount; i++) {

            CombatArgs args = new CombatArgs();
            args.skill = skill;
            args.element = skill.element;
            args.target = target;
            args.user = user;
            args.source = this;

            foreach (var effect in skill.skillEffects) {
                effect.Prepare(args);
            }

            var ui = CombatManager.instance.view;
            if (skillParticle)
                UnityEngine.Object.Instantiate(skillParticle, ui.GetCharacterWorldPosition(args.target), Quaternion.identity);
            else
                Debug.Log("No Particle, add it to: " + skill.skillName);
            yield return new WaitForSeconds(damageDelay);
            args.Resolve();
            yield return new WaitForSeconds(hitDelay);
        }

        yield return new WaitForSeconds(1.2f);
    }

    public bool ValidateTarget(Character user, Character target) {
        bool sameTeam = user.team == target.team;
        bool alive = target.Health.Current > 0;
        return sameTeam ^ targetEnemy && alive ^ targetDead;
    }
}
