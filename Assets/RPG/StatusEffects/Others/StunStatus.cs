using System;
using TurnBasedRPG;
using TurnBasedRPG.StatusEffects;

[Serializable, Obsolete]
public class StunStatus : Status {
    public override IObservable<int> DisplayValue => throw new Exception();
    public Observable<int> duration = new (1);
    
    public override void Apply(Character target) => throw new NotImplementedException();
    public override void Remove(Character target) => throw new NotImplementedException();
    public override void Stack(Character target, Status other) => throw new NotImplementedException();
}
