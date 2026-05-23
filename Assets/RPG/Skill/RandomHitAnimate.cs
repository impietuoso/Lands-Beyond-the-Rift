using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.TargetFilters;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable, Obsolete]
public class RandomHitAnimate : ISkillAnimationOld {
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
            area = TargetArea.All,
            filter = targetDead ? new Dead() : new Alive()
        };
    }

    public IEnumerator Play(Skill skill, Character user, Character target, CombatManager cm) {
        var ui = CombatManager.instance.view;
        if(castingParticle) {
            var particle = UnityEngine.Object.Instantiate(
                castingParticle,
                ui.GetCharacterWorldPosition(user),
                Quaternion.identity);
            yield return new WaitWhile(() => particle);
        }
        else {
            yield return new WaitForSeconds(0.1f);
        }

        int newHitCount = Random.Range(hitCount.x, hitCount.y + 1);

        List<Character> targets = new(GetAffectedTargets(user, target));
        yield return cm.StartCoroutine(SingleTargetDamage(skill, user, targets, newHitCount));

        if(hitCount.y > 1) Debug.Log(newHitCount + " Hits");
    }

    public IEnumerable<Character> GetAffectedTargets(Character user, Character target) {
        foreach (var newTarget in target.cm.everyone) {
            if(ValidateTarget(user, newTarget)) yield return newTarget;
        }
    }

    public IEnumerator SingleTargetDamage(Skill skill, Character user, List<Character> targets, int newHitCount) {
        for (int i = 0; i < newHitCount; i++) {
            CombatArgs args = new CombatArgs();
            args.skill = skill;
            args.element = skill.element;
            args.target = targets[Random.Range(0, targets.Count)];
            args.user = user;
            args.source = this;

            foreach (var effect in skill.skillEffects) {
                effect.Prepare(args);
            }

            var ui = CombatManager.instance.view;
            UnityEngine.Object.Instantiate(skillParticle, ui.GetCharacterWorldPosition(args.target), Quaternion.identity);
            yield return new WaitForSeconds(damageDelay);

            args.Resolve();
            yield return new WaitForSeconds(hitDelay);
        }
    }

    public bool ValidateTarget(Character user, Character target) {
        bool sameTeam = user.team == target.team;
        bool alive = target.Health.Current > 0;
        return sameTeam ^ targetEnemy && alive ^ targetDead;
    }
}