using System;
using System.Collections;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.Data;
using TurnBasedRPG.StatusEffect;
using UnityEngine;
using UnityEngine.Scripting;
using Random = UnityEngine.Random;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Preserve, Serializable]
    public class ApplyEffects : ISkillEffect {
        public StatusSO[] statuses;
        public int hitChance = 100;
        public bool targetUser;
        public bool unavoidable;
        public bool applyOnce = true;

        public void Prepare(CombatArgs args) {
            args.hitChance = hitChance;
            args.unavoidable = unavoidable;
            args.OnResolve += TryApply;
        }

        private void TryApply(CombatArgs args) {
            var target = targetUser ? args.user : args.target;
            var flag = (target, this);
            if(applyOnce && !args.cm.actionFlags.Add(flag)) return;

            var hit = Random.Range(0, 100) <= 100 - target[Stat.Resistance];
            if(!hit) args.result.ResistStatus = true;

            var applyIe = new GenericCombatEvent(Apply(hit, target, statuses));
            args.cm.combatEvents.Enqueue(applyIe);
        }

        private static IEnumerator Apply(bool hit, Character tgt, StatusSO[] effects) {
            yield return null;
            foreach (var effect in effects) {
                if(!hit && effect.statusType == StatusType.Debuff) continue;
                Debug.Log(effect.status.statusName + " was apply to " + tgt.characterName);
                tgt.StatusEffectList.Apply(effect);
                yield return new WaitForSeconds(.5f);
            }
        }
    }
}