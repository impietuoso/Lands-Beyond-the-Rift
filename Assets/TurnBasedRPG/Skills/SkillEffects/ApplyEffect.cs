using System;
using System.Collections;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.Data;
using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.StatusEffects;
using UnityEngine;
using UnityEngine.Scripting;
using Random = UnityEngine.Random;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class ApplyEffect : ISkillEffect, ISingleLineDrawer {
        public StatusSO status;
        [Label("tgtUsr")] public bool targetUser;

        public void Prepare(CombatArgs args) {
            args.OnResolve += TryApply;
        }

        private void TryApply(CombatArgs args) {
            var target = targetUser ? args.user : args.target;
            var flag = (target, this);
            if(!args.cm.ActionFlags.Add(flag)) return;
            var applyIe = Apply(target, status);

            if(status.Type != StatusType.Debuff) {
                args.cm.CombatEvents.Enqueue(applyIe);
                return;
            }

            var applyChance = Random.Range(0, 100);
            if(applyChance > 100 - target[Stat.Resistance]) {
                args.result.ResistStatus = true;
                return;
            }

            args.cm.CombatEvents.Enqueue(applyIe);
        }

        private static IEnumerator Apply(Character tgt, StatusSO effect) {
            yield return null;
            Debug.Log(effect.DisplayName + " was apply to " + tgt.characterName);
            tgt.StatusEffectList.Apply(effect);
        }
    }
}