using TurnBasedRPG.BattleStats;
using TurnBasedRPG.Data;
using UnityEditor;

namespace TurnBasedRPG.Editor {
    [CustomEditor(typeof(PartyMember))]
    public class PartyMemberEditor : UnityEditor.Editor {
        private Stats publicStats = new();
        public bool applyEquipmentStats;

        public void OnEnable() {
            var member = (PartyMember)target;
            var statBonus = applyEquipmentStats ? member : Stats.Zero;
            publicStats.Recalculate(member.level, member, statBonus);
        }

        public override void OnInspectorGUI() {
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Derived Stats (Preview)", EditorStyles.boldLabel);

            applyEquipmentStats = EditorGUILayout.Toggle("Apply Equipment Stats", applyEquipmentStats);
            var change = EditorGUI.EndChangeCheck();

            EditorGUI.BeginDisabledGroup(true);
            foreach (var stat in Stats.All)
                EditorGUILayout.IntField(stat.ToString(), publicStats[stat]);
            EditorGUI.EndDisabledGroup();

            if(!change) return;
            var member = (PartyMember)target;
            var statBonus = applyEquipmentStats ? member : Stats.Zero;
            publicStats.Recalculate(member.level, member, statBonus);
        }
    }
}