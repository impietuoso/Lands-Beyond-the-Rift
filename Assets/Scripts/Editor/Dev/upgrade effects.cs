using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;

namespace Editor.Dev {
    public class upgrade_effects {
        [MenuItem("Tools/Dev/Upgrade Skill Effects")]
        public static void Upgrade() {
            var missingUpgrades = new HashSet<Type>();

            var guids = AssetDatabase.FindAssets("t:Skill");
            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var skill = AssetDatabase.LoadAssetAtPath<Skill>(path);
                if(!skill || skill.effects == null || skill.effects.Count == 0) continue;

                if(skill.animation is ISkillAnimationOld a) {
                    var t = a.GetTargeting(skill);
                    if(t != null && t != skill.targeting) {
                        skill.targeting = t;
                        EditorUtility.SetDirty(skill);
                    }
                }

                for (var i = 0; i < skill.effects.Count; i++) {
                    var oldEffect = skill.effects[i];
                    var upgrade = oldEffect?.GetUpgrade(skill);
                    if(upgrade == oldEffect) continue;
                    if(upgrade == null) {
                        missingUpgrades.Add(oldEffect.GetType());
                        continue;
                    }

                    skill.effects[i] = upgrade;
                    EditorUtility.SetDirty(skill);
                }
            }

            foreach (var type in missingUpgrades)
                Debug.LogWarning($"module {type.FullName} missing upgrade");

            AssetDatabase.SaveAssets();
            Debug.Log("Skill effect upgrade complete.");
        }
    }
}