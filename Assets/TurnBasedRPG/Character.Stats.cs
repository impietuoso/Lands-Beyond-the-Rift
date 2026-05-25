using TurnBasedRPG.BattleStats;
using TurnBasedRPG.StatusEffects;
using UnityEngine;

namespace TurnBasedRPG
{
    public partial class Character : IAttributes, IStats {
        [Header("Stats")]
        [field: SerializeField] public ResourceStat Health { get; private set; } = new ();
        [field: SerializeField] public ResourceStat Shield { get; private set; } = new ();
        [field: SerializeField] public ResourceStat Mana { get; private set; } = new ();
        [field: SerializeField] public StatsSummary Stats { get; private set; }
        [field: SerializeField] public BoolStat Stun { get; private set; } = new();
        [field: SerializeField] public BoolStat Silence { get; private set; } = new();

        public int this[Attribute a] => member[a];
        public int this[Stat s] => Stats[s];
        
        private void InitializeStats() {
            Stats = new ();
            StatusEffectList = new StatusEffectList(this);

            Stats.MaxHealth.OnChanged += v => Health.Max = v;
            Stats.MaxShield.OnChanged += v => Shield.Max = v;
            Stats.MaxMana.OnChanged += v => Mana.Max = v;

            Stats.Recalculate(member.Level, member, member);
            Health.Current = Health.Max;
            Mana.Current = Mana.Max;
        }
    }
}