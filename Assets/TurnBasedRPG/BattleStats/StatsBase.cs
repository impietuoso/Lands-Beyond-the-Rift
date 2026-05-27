using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;

namespace TurnBasedRPG.BattleStats {
    public enum Stat {
        MaxHealth,
        MaxShield,
        MaxMana,
        Speed,
        Hit,
        Evade,
        Armor,
        Resistance,
        Damage,
        CritChance,
        CritDamage,
    }

    public interface IStats {
        int this[Stat stat] { get; }
    }

    [Serializable]
    public class StatsBase : IStats, ITwoColumnsDrawer {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField, HideInInspector] public int MaxShield { get; private set; }
        [field: SerializeField] public int MaxMana { get; private set; }
        [field: SerializeField] public int Speed { get; private set; }
        [field: SerializeField] public int Hit { get; private set; }
        [field: SerializeField] public int Evade { get; private set; }
        [field: SerializeField] public int Armor { get; private set; }
        [field: SerializeField] public int Resistance { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int CritChance { get; private set; }
        [field: SerializeField] public int CritDamage { get; private set; }

        public int this[Stat stat] => stat switch
        {
            Stat.MaxHealth => MaxHealth,
            Stat.MaxShield => MaxShield,
            Stat.MaxMana => MaxMana,
            Stat.Speed => Speed,
            Stat.Hit => Hit,
            Stat.Evade => Evade,
            Stat.Armor => Armor,
            Stat.Resistance => Resistance,
            Stat.Damage => Damage,
            Stat.CritChance => CritChance,
            Stat.CritDamage => CritDamage,
            _ => throw new NotImplementedException()
        };
    }
}