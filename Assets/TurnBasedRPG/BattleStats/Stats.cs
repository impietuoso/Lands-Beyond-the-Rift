using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static TurnBasedRPG.BattleStats.Attribute;
using static TurnBasedRPG.BattleStats.Stat;

namespace TurnBasedRPG.BattleStats {
    [Serializable]
    public class Stats : IStats {
        public static int Count => All.Count;
        public static IReadOnlyList<Stat> All { get; } = Enum.GetValues(typeof(Stat)).OfType<Stat>().ToArray();
        public static IStats Zero { get; } = new StatsBase();

        [SerializeField] private BonusStat[] stats;

        public Stats() {
            stats = new BonusStat[Count];
            for (var i = 0; i < stats.Length; i++)
                stats[i] = new ();

            stats[(int)MaxHealth].Max = 9999;
            stats[(int)MaxShield].Max = 9999;
            stats[(int)MaxMana].Max = 9999;
            stats[(int)Evade].Max = 40;
            stats[(int)Resistance].Max = 60;
        }

        public int this[Stat stat] => stats[(int)stat].Total;
        public BonusStat GetStat(Stat stat) => stats[(int)stat];

        public void Recalculate(int level, IAttributes a, IStats bonus) =>
            new StatCalcHelper(this).Recalculate(level, a, bonus);
    }

    public class StatCalcHelper : IStats {
        private readonly Stats _context;
        private readonly int[] _v = new int[12];
        public StatCalcHelper(Stats context) => _context = context;
        public int this[Stat s] { get => _v[(int)s]; private set => _v[(int)s] = value; }

        public void Recalculate(int level, IAttributes a, IStats stats) {
            this[MaxHealth] = 2 * level + a[Vit] * 15;
            this[MaxMana] = 6 * level + a[Spt] * 8;
            this[MaxShield] = this[MaxHealth] / 2;
            this[Speed] = level + a[Dex] * 4;
            this[Evade] = a[Dex] * 100 / (a[Dex] + 40);
            this[Resistance] = (a[Vit] * 2 + a[Spt]) * 100 / (a[Vit] * 2 + a[Spt] + 60);
            this[Damage] = 100;
            this[CritDamage] = 150;

            foreach (var s in Stats.All)
                _v[(int)s] += stats[s];

            foreach (var s in Stats.All)
                _context.GetStat(s).Base = _v[(int)s];
        }
    }
}