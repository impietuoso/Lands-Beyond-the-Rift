using System;
using System.Collections;
using System.Linq;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;


[Serializable]
public class OnTakeDamage : IPassive {
    public Element element;
    public ISkill counterSkill;
    public bool castOnSelf;

    public void Subscribe(Character character) {
        character.OnResolveDefend += ConsequencesOfSeuActs;
    }

    public void Unsubscribe(Character character) {
        character.OnResolveDefend -= ConsequencesOfSeuActs;
    }

    public void ConsequencesOfSeuActs(CombatArgs args) {
        if(element && args.element != element) return;
        if(args.result.Health.Delta >= 0) return;
        if(args.stopReactionAttacks) return;
        if(!args.cm.ActionFlags.Add((this, args.target))) return;

        var combatEvent = Execute(args, this);
        args.cm.CombatEvents.Enqueue(combatEvent);
    }

    public IEnumerator Execute(CombatArgs args, OnTakeDamage e) {
        yield return null;
        var tgt = e.castOnSelf ? args.target : args.user;
        yield return e.counterSkill.UseSkill(args.target, tgt);
    }
}