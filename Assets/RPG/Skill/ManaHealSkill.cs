using System;
using TurnBasedRPG;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using UnityEngine;

[Serializable, Obsolete]
public class ManaHealSkill : ISkillEffectOld
{
    //valor de mana é multiplicado por variavel fixa ou dano causado
    [Range(0f, 1f)]
    public float manaHealPercentage = 0.2f;

    public void Prepare(CombatArgs args)
    {
        args.OnResolve += HealMana;
    }

    public void HealMana(CombatArgs args)
    {
        args.OnResolve -= HealMana; ////manaHeal
        var heal = args.user.Mana.Max * manaHealPercentage;
        if (heal == 0 || args.result.miss) return;
        args.user.Mana.Current += (int)heal;
    }
}