using System;
using TurnBasedRPG;
using TurnBasedRPG.StatusEffects;


[Serializable, Obsolete]
public class SpeedStatus : Status {
    public override IObservable<int> DisplayValue => throw new Exception();
    public Observable<int> duration = new (3);
    public float multiplier = 1.3f;

    public override void Apply(Character target) {
        if (TryNullifyOpposite(target)) return;
        target.Stats.Speed.AddBonus(this, multiplier);
        target.OnEndTurn += OnTurnEnd;
    }

    public override void Remove(Character target) {
        target.Stats.Speed.RemoveBonus(this);
        target.OnEndTurn -= OnTurnEnd;
    }

    public override void Stack(Character target, Status other) {
        if (other is SpeedStatus otherStatus) {
            duration.Value = otherStatus.duration.Value;
        }
    }

    private void OnTurnEnd(Character target) {
        duration.Value--;
        if (duration.Value <= 0) {
            target.StatusEffectList.Remove(Source);
        }
    }
}