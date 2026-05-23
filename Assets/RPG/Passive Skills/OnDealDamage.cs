using System;
using System.Collections;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;

[Serializable]
public class OnDealDamage : IPassive {
    public Element element;
    public Skill selfUseSkill;
    public bool castOnSelf;

    public void Subscribe(Character character) {
        character.OnResolveAttack += ConsequencesOfSeuActs;
    }

    public void Unsubscribe(Character character) {
        character.OnResolveAttack -= ConsequencesOfSeuActs;
    }

    private void ConsequencesOfSeuActs(CombatArgs args) {
        if(element && args.element != element) return;
        if(args.result.Health.Delta >= 0) return;
        if(args.stopReactionAttacks) return;
        if(!args.cm.ActionFlags.Add((this, args))) return;

        var combatEvent = Execute(args, this);
        args.cm.CombatEvents.Enqueue(combatEvent);
    }

    private IEnumerator Execute(CombatArgs args, OnDealDamage e) {
        yield return null;
        var tgt = e.castOnSelf ? args.user : args.target;
        yield return e.selfUseSkill.UseSkill(args.user, tgt);
    }
}