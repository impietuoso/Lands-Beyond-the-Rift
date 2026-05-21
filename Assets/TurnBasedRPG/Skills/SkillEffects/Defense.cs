using System;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;
using UnityEngine.Scripting;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class Defense : ISkillEffect, ISingleLineDrawer {
        [Range(0f, 1f)] public float damageReduction;

        public void Prepare(CombatArgs args) {
            args.user.OnStartTurn += OnStartTurn;
            args.user.OnDefend += OnDefend;
            args.unavoidable = true;
        }

        private void OnDefend(CombatArgs args) {
            args.damage = (int)(args.damage * (1f - damageReduction));
        }

        private void OnStartTurn(Character target) {
            target.OnStartTurn -= OnStartTurn;
            target.OnDefend -= OnDefend;
        }
    }
}