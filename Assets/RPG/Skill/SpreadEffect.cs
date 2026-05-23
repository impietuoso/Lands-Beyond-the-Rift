using System;
using System.Collections;
using TurnBasedRPG;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills;
using UnityEngine;


[Serializable, Obsolete]
public class SpreadEffect : ISkillEffectOld {
    [SerializeReference, TypeDropdown(typeof(ISkillEffectOld))]
    public ISkillEffectOld effect;
    public float spreadDelay;

    public void Prepare(CombatArgs args) {
        foreach (var tgt in args.cm.Everyone) {
            if(!ValidateTarget(args.target, tgt)) continue;
            var chain = args.Chain(args.source, tgt);
            chain.unavoidable = true;
            effect.Prepare(chain);
            args.OnResolve += _ => args.cm.StartCoroutine(ResolveSpread(chain));
        }
    }

    private IEnumerator ResolveSpread(CombatArgs newArgs) {
        yield return new WaitForSeconds(spreadDelay);
        newArgs.Resolve();
    }

    private bool ValidateTarget(Character user, Character target) {
        var sameTeam = user.team == target.team;
        var alive = target.Health.Current > 0;
        var notSelf = target != user;
        return sameTeam && alive && notSelf;
    }
}