using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.TargetFilters;
using UnityEngine;


[Serializable, Obsolete]
public class SelfHitAnimate : ISkillAnimationOld {
    public float damageDelay = 1;
    public GameObject castingParticle;
    public GameObject skillParticle;

    public Targeting GetTargeting(Skill skill) {
        return new Targeting
        {
            group = TargetGroup.Self,
            area = TargetArea.One,
            filter = new Alive()
        };
    }

    public bool TrySkipSelection(Character user, Skill skill) => throw new NotImplementedException();

    public bool ValidateTarget(Character user, Character target) {
        if (user == target) return true;
        else return false;
    }

    public IEnumerable<Character> GetAffectedTargets(Character user, Character target) {
        yield return target;
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

        Debug.Log(user.characterName + " Defends!");
        CombatArgs args = new CombatArgs();
        args.skill = skill;
        args.element = skill.element;
        args.user = user;
        args.target = user;
        args.source = this;

        foreach (var effect in skill.skillEffects) {
            effect.Prepare(args);
        }

        UnityEngine.Object.Instantiate(skillParticle, ui.GetCharacterWorldPosition(args.target), Quaternion.identity);
        yield return new WaitForSeconds(damageDelay);

        args.Resolve();
    }
}
