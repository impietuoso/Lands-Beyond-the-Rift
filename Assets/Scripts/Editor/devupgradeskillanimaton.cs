using System;
using System.Linq;
using RPG;
using UnityEditor;
using UnityEngine;
using TurnBasedRPG.Skills;
using System.Collections.Generic;

namespace Editor {
    public class devupgradeskillanimaton {
        [MenuItem("Tools/RPG/Upgrade Skill Animations")]
        public static void UpgradeSkillAnimations() {
            var skillGuids = AssetDatabase.FindAssets("t:Skill");
            var count = 0;
            var dic = new HashSet<Type>();

            foreach (var guid in skillGuids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var skill = AssetDatabase.LoadAssetAtPath<Skill>(path);
                if(!skill) continue;

                var changed = false;

                Item item = skill;
                if(item.description != skill.Description || item.displayName != skill.SkillName) {
                    item.ReflectSet("description", skill.Description);
                    item.ReflectSet("displayName", skill.SkillName);
                    item.ReflectSet("sprite", skill.Icon);
                    changed = true;
                }
                
                for (var i = 0; i < skill.effects.Count; i++) {
                    var e = skill.effects[i];
                    if(e is ISkillEffect) continue;

                    var newEffect = e.GetUpgrade(skill);
                    if(newEffect != null && newEffect != e) {
                        Undo.RecordObject(skill, "Upgrade Skill Effect");
                        skill.effects[i] = newEffect;
                        changed = true;
                    }
                    else {
                        Debug.LogError($"Skill '{skill.SkillName}' at {path} contains legacy effects that could not be upgraded.", skill);
                        dic.Add(e.GetType());
                    }
                }

                if(skill.animation is ISkillAnimationOld oldAnim) {
                    var newAnim = oldAnim.GetUpgrade(skill);

                    if(newAnim != null && newAnim != skill.animation) {
                        Undo.RecordObject(skill, "Upgrade Skill Animation");
                        skill.animation = newAnim;
                        changed = true;
                    }
                }

                if(changed) {
                    EditorUtility.SetDirty(skill);
                    count++;
                }
            }

            AssetDatabase.SaveAssets();
            Debug.Log($"Upgraded {count} skills.");

            foreach (var type in dic)
                Debug.LogError($"Legacy effect type found: {type.Name}");
        }
    }
}