using System.Linq;
using UnityEditor;
using UnityEngine;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.SkillEffects;
using TurnBasedRPG.Skills.DamageModifiers;

namespace Editor.Dev {
    public class upgrade_effects {
        [MenuItem("Tools/Dev/Upgrade Skill Effects")]
        public static void Upgrade() {
            var guids = AssetDatabase.FindAssets("t:Skill");
            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var skill = AssetDatabase.LoadAssetAtPath<Skill>(path);
                if(!skill || skill.skillEffects == null || skill.skillEffects.Count == 0) continue;

                var changed = false;
                var oldDmg = skill.skillEffects.OfType<DamageSkill>().FirstOrDefault();
                var oldHeal = skill.skillEffects.OfType<HealSkill>().FirstOrDefault();

                // Map legacy status effects to new Damage Modifiers
                var statuses = skill.skillEffects.OfType<ApplyStatusEffect>().Select(ase => (ase.status, ase.targetUser)).ToList();
                skill.skillEffects.RemoveAll(e => e is ApplyStatusEffect);

                if(oldDmg != null) {
                    var statusModifiers = statuses.Select(s => new ApplyStatus { status = s.status, targetUser = s.targetUser })
                        .Cast<IDamageModifier>().ToArray();

                    if(oldDmg.isPercentageDamage) {
                        skill.effect = new PercentDamage
                        {
                            percent = oldDmg.healthPercentage,
                            hitChance = oldDmg.hitChance,
                            modifiers = statusModifiers,
                        };
                    }
                    else {
                        skill.effect = new Damage
                        {
                            damage = oldDmg.baseDamage,
                            scale = new AttributeScale
                            {
                                scale = oldDmg.statMultiplier,
                                attribute = oldDmg.damageStatScale
                            },
                            hitChance = oldDmg.hitChance,
                            critChance = oldDmg.criticalChance,
                            damageRange = oldDmg.damageRange,
                            modifiers = statusModifiers,
                        };
                    }

                    skill.skillEffects.Remove(oldDmg);
                    changed = true;
                }
                else if(oldHeal != null) {
                    if(oldHeal.isPercentageHeal) {
                        skill.effect = new HealPercent
                        {
                            percent = oldHeal.healthPercentage
                        };
                    }
                    else {
                        skill.effect = new Heal
                        {
                            amount = oldHeal.healAmount,
                            scale = new AttributeScale
                            {
                                attribute = oldHeal.healStatScale,
                                scale = oldHeal.statMultiplier
                            }
                        };
                    }

                    skill.skillEffects.Remove(oldHeal);
                    changed = true;
                }
                else {
                    if(statuses.Count == 1) {
                        skill.effect = new ApplyEffect
                        {
                            status = statuses[0].status,
                            targetUser = statuses[0].targetUser,
                        };
                    }

                    if(statuses.Count > 1) {
                        skill.effect = new ApplyEffects
                        {
                            statuses = statuses.Select(s => s.status).ToArray(),
                            targetUser = statuses[0].targetUser,
                        };
                    }
                }

                if(changed)
                    EditorUtility.SetDirty(skill);
                else
                    Debug.LogWarning($"skill: {skill.name} not changed");

                if(oldHeal != null && oldDmg != null)
                    Debug.LogError($"Skill {skill.name} has both Heal and Damage - manual check required.");
            }

            AssetDatabase.SaveAssets();
            Debug.Log("Skill effect upgrade complete.");
        }
    }
}