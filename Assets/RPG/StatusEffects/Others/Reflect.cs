using System;
using System.Collections;
using TurnBasedRPG;
using TurnBasedRPG.StatusEffects;
using UnityEngine;

[System.Serializable, Obsolete]
public class ReflectEffect : Status {
    [Range(0,10)]
    public float reflectPercentage;
    public bool protectFromDamage;
    public override IObservable<int> DisplayValue => throw new Exception();
    public Observable<int> duration = new (3);
    public override void Apply(Character target) {
        if (TryNullifyOpposite(target)) return;
        target.OnEndTurn += OnTurnEnd;
        target.OnDefend += OnTakeDamage;
    }

    public override void Remove(Character target) {
        target.OnEndTurn -= OnTurnEnd;
    }

    public override void Stack(Character target, Status other) {
        if (other is ReflectEffect otherStatus) {
            duration.Value = otherStatus.duration.Value;
        }
    }

    private void OnTakeDamage(CombatArgs args) {
        if (args.user == null || args.stopReactionAttacks) return;
        var damageReflected = (int)(args.damage * reflectPercentage);
        args.damage -= protectFromDamage ? damageReflected : 0;
        
        var chain = args.Chain(this, args.user);
        chain.unavoidable = true;
        chain.damage = damageReflected;
        var genericEvent = ReflectDamage(chain);
        args.cm.CombatEvents.Enqueue(genericEvent);
    }
    
    public IEnumerator ReflectDamage (CombatArgs args) {
        yield return null;
        args.Resolve();
        Debug.Log(args.target.characterName + " takes " + args.damage + " reflect damage.");
    }
    
    private void OnTurnEnd(Character target) {
        duration.Value--;
        if (duration.Value <= 0) {
            target.StatusEffectList.Remove(Source);
        }
    }
}