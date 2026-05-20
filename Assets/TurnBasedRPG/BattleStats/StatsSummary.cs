using System;

[Serializable]
public class StatsSummary : Stats
{
    public BonusStat MaxHealth => GetStat(Stat.MaxHealth);
    public BonusStat MaxShield => GetStat(Stat.MaxShield);
    public BonusStat MaxMana => GetStat(Stat.MaxMana);

    public BonusStat Speed => GetStat(Stat.Speed);
    // public BonusStat Hit => GetStat(Stat.Hit);
    // public BonusStat Evade => GetStat(Stat.Evade);
    public BonusStat Armor => GetStat(Stat.Armor);
    // public BonusStat Resistance => GetStat(Stat.Resistance);

    // public BonusStat Damage => GetStat(Stat.Damage);
    // public BonusStat CritChance => GetStat(Stat.CritChance);
    // public BonusStat CritDamage => GetStat(Stat.CritDamage);
    // public BonusStat CastSpeed => GetStat(Stat.CastSpeed);
}