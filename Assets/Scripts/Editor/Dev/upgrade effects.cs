using System.Linq;
using UnityEditor;
using UnityEngine;
using TurnBasedRPG.Data;
using TurnBasedRPG.Skills;
using TurnBasedRPG.Skills.SkillEffects;

namespace Editor.Dev {
    public class upgrade_effects {
        [MenuItem("Tools/Dev/Upgrade Skill Effects")]
        public static void Upgrade() {
            var guids = AssetDatabase.FindAssets("t:Skill");
            foreach (var guid in guids) {
                var path = AssetDatabase.GUIDToAssetPath(guid);
                var skill = AssetDatabase.LoadAssetAtPath<Skill>(path);
                if(!skill || skill.skillEffects == null || skill.skillEffects.Count == 0) continue;

                for (var i = 0; i < skill.skillEffects.Count; i++) {
                    var oldEffect = skill.skillEffects[i];
                    if(oldEffect == null) continue;
                    if(oldEffect is ICombatEffect) continue;
                    var upgrade = oldEffect.GetUpgrade(skill);
                    if(upgrade == null) {
                        Debug.LogWarning($"skill: {skill.name} module {oldEffect.GetType().Name} missing", skill);
                        continue;
                    }

                    skill.skillEffects[i] = upgrade;
                    EditorUtility.SetDirty(skill);
                }
            }

            AssetDatabase.SaveAssets();
            Debug.Log("Skill effect upgrade complete.");
        }
    }
}