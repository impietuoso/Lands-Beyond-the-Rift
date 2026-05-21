using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class Damage : ICombatEffect {
        public int damage = 10;
        public AttributeScale scale;
        [Range(0, 100)] public int hitChance = 80;
        [Range(0, 100)] public int critChance = 10;
        [Range(0, 1)] public float damageRange = 0.15f;
        [SerializeReference, TypeInstance] public IDamageModifier[] modifiers;

        void ISkillEffect.Prepare(CombatArgs args) {
            var value = damage + scale.GetValue(args.user);
            var range = UnityEngine.Random.Range(1 - damageRange, 1 + damageRange);
            value = Mathf.RoundToInt(value * range);

            args.damage = value;
            args.critChance = critChance;
            args.hitChance = hitChance;

            foreach (var mod in modifiers)
                mod.ModifyArgs(args);
        }
    }
}