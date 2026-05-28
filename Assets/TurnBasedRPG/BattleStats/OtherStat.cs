using System;
using System.Collections.Generic;
using System.Linq;

namespace TurnBasedRPG.BattleStats {
    public enum OtherStat {
        Malignance, // inc odds of applying debuffs
        ManaCost, // multiply mana cost
        BuffDuration,
        DebuffDuration,
    }

    public interface IOtherStats {
        int this[OtherStat os] { get; }
    }

    public class OtherStats : IOtherStats {
        public static IReadOnlyList<OtherStat> All { get; } = Enum.GetValues(typeof(OtherStat)).OfType<OtherStat>().ToArray();
        public static IOtherStats Zero { get; } = new OtherStats();

        private readonly BonusStat[] _stats;

        public OtherStats() {
            _stats = new BonusStat[All.Count];
            for (var i = 0; i < _stats.Length; i++)
                _stats[i] = new();
        }

        public int this[OtherStat stat] => _stats[(int)stat].Total;
        public BonusStat GetStat(OtherStat stat) => _stats[(int)stat];
    }
}