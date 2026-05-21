using System;
using System.Collections;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.Skills;
using UnityEngine;
[Serializable]
public class SpreadEffect : ISkillEffect {
    [SerializeReference, TypeDropdown(typeof(ISkillEffect))]
    public ISkillEffect effect;
    public float spreadDelay;

    public void Prepare(CombatArgs args) {
        foreach (var newTarget in CombatManager.instance.characterList) {
            if (ValidateTarget(args.target, newTarget)) {
                var newArgs = args.Chain(args.source, newTarget);
                newArgs.unavoidable = true;
                effect.Prepare(newArgs);
                args.OnResolve += _=> CombatManager.instance.StartCoroutine(ResolveSpread(newArgs));
            }
        }
    }

    public IEnumerator ResolveSpread(CombatArgs newArgs) {
        yield return new WaitForSeconds(spreadDelay);
        newArgs.Resolve();
    }
    
    public bool ValidateTarget(Character user, Character target) {
        bool sameTeam = user.team == target.team;
        bool alive = target.Health.Current > 0;
        bool notSelf = target != user;
        return sameTeam && alive && notSelf;
    }
}