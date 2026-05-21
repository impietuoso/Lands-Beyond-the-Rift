using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

            stats[(int)Stat.MaxHealth].Max = 9999;
            stats[(int)Stat.MaxShield].Max = 9999;
            stats[(int)Stat.MaxMana].Max = 9999;
            stats[(int)Stat.Evade].Max = 40;
            stats[(int)Stat.Resistance].Max = 60;
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
            this[Stat.MaxHealth] = 2 * level + a[Attribute.Vit] * 15;
            this[Stat.MaxMana] = 6 * level + a[Attribute.Spt] * 8;
            this[Stat.MaxShield] = this[Stat.MaxHealth] / 2;
            this[Stat.Speed] = level + a[Attribute.Dex] * 4;
            this[Stat.Evade] = a[Attribute.Dex] * 100 / (a[Attribute.Dex] + 40);
            this[Stat.Resistance] = (a[Attribute.Vit] * 2 + a[Attribute.Spt]) * 100 / (a[Attribute.Vit] * 2 + a[Attribute.Spt] + 60);
            this[Stat.CritDamage] = 150;

            foreach (var s in Stats.All)
                _v[(int)s] += stats[s];

            foreach (var s in Stats.All)
                _context.GetStat(s).Base = _v[(int)s];
        }
    }
}