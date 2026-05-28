using System;
using System.Collections;
using System.Collections.Generic;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.Data;
using TurnBasedRPG.StatusEffects;
using UnityEngine;
using static TurnBasedRPG.BattleStats.OtherStat;
using Random = UnityEngine.Random;
using static TurnBasedRPG.BattleStats.Stat;

namespace TurnBasedRPG {
    public class CombatArgs {
        public object source;
        public Character user;
        public Character target;
        public ISkill skill;
        public Element element;

        public bool unavoidable;
        public bool ignoreShield;
        public bool ignoreArmor;
        public bool stopReactionAttacks;
        public bool cannotCrit;

        public int damage;
        public int heal;
        public int mana;
        public int shield;
        public int critChance;
        public int hitChance;

        public CombatResult result;
        public List<StatusSO> statusEffects = new();
        public Action<CombatArgs> OnResolve;

        public CombatManager cm => target.cm;

        public void Resolve() {
            if(result != null) return;
            if(target == null) throw new Exception("No Target");

            // setup events
            user?.OnAttack?.Invoke(this);
            target.OnDefend?.Invoke(this);

            // rng
            hitChance += -target[Evade] + (user?[Hit] ?? 0);
            var hitRoll = Random.Range(0, 100);
            var critRoll = Random.Range(0, 100);
            var hit = unavoidable || hitRoll < hitChance;
            var crit = hit && !cannotCrit && user != null && critRoll < critChance;

            // result info
            result = new CombatResult
            {
                Miss = !hit,
                Crit = crit,
            };

            // armor reduction
            if(!ignoreArmor) damage = Mathf.Max(damage - target[Armor], 0);

            // damage stat
            if(user != null) damage = (int)(damage * user[Stat.Damage] / 100f);

            // crit damage
            if(crit) {
                var critDmg = user[CritDamage] / 100f;
                damage = (int)(damage * critDmg);
            }

            if(!hit) damage = 0;

            // shield delta
            if(hit && !ignoreShield) {
                result.Shield = target.Shield.Add(shield - damage);
                if(result.Shield.Delta < 0) damage += result.Shield.Delta;
            }
            else
                result.Shield = new(target.Shield.Current);

            // element bonus
            if(hit && element) {
                if(target.Element.weak.Contains(element))
                    damage = (int)(damage * 1.2f);
                else if(element.weak.Contains(target.Element))
                    damage = (int)(damage * 0.8f);
            }

            // delta hp & mp
            result.Health = target.Health.Add(heal - damage);
            result.Mana = target.Mana.Add(mana);

            // resolve events
            user?.OnResolveAttack?.Invoke(this);
            target.OnResolveDefend?.Invoke(this);
            OnResolve?.Invoke(this);

            if(result.Miss) Debug.Log("Miss");
        }

        public void TryApplyStatuses(Character tgt, params StatusSO[] statuses) {
            var roll = Random.Range(0, 100);
            var chance = 100 - tgt[Resistance] + (user?[Malignance] ?? 0);
            var apply = roll < chance;

            foreach (var so in statuses) {
                var flag = (target: tgt, so);
                if(!cm.ActionFlags.Add(flag)) continue;

                if(so.Type != StatusType.Debuff) {
                    cm.CombatEvents.Enqueue(Apply(tgt, so));
                    continue;
                }

                if(!apply) {
                    result.ResistStatus = true;
                    continue;
                }

                cm.CombatEvents.Enqueue(Apply(tgt, so));
            }
        }

        private static IEnumerator Apply(Character tgt, StatusSO effect) {
            yield return null;
            Debug.Log(effect.DisplayName + " was apply to " + tgt.characterName);
            tgt.StatusEffectList.Apply(effect);
        }

        public CombatArgs Chain() => Chain(source, target);

        public CombatArgs Chain(object src, Character tgt) => new()
        {
            source = src ?? source,
            skill = skill,
            element = element,
            user = user,
            target = tgt,
            stopReactionAttacks = true,
        };
    }

    public class CombatResult {
        public ResourceStat.Result Health;
        public ResourceStat.Result Mana;
        public ResourceStat.Result Shield;
        public bool Crit;
        public bool Miss;
        public bool ResistStatus;
        public bool IsFatal => Health.Fatal;
        public bool IsRevive => Health.Revive;

        public int TotalDamage
        {
            get {
                var total = Health.Delta + Shield.Delta;
                return total >= 0 ? 0 : -total;
            }
        }

        [Obsolete] public int deltaHp;
        [Obsolete] public int deltaMp;
        [Obsolete] public int deltaShield;
        [Obsolete] public bool isCrit;
        [Obsolete] public bool isFatal;
        [Obsolete] public bool isRevive;
        [Obsolete] public bool miss;
        [Obsolete] public bool resistStatus;
    }
}