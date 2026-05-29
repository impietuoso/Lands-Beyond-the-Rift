using Data;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.Data;
using UnityEditor;

namespace Editor {
    [CanEditMultipleObjects]
    [CustomEditor(typeof(EnemyInfo))]
    public class EnemyInfoEditor : UnityEditor.Editor {
        private Stats publicStats = new();

        public void OnEnable() {
            var member = (IPartyMember)target;
            publicStats.Recalculate(member.Level, member, member);
        }

        public override void OnInspectorGUI() {
            EditorGUI.BeginChangeCheck();
            base.OnInspectorGUI();
            var member = (IPartyMember)target;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Derived Stats (Preview)", EditorStyles.boldLabel);

            var unused = member.GetUnusedPoints();
            EditorGUILayout.LabelField("Unused points: " + unused, EditorStyles.boldLabel);

            var change = EditorGUI.EndChangeCheck();

            EditorGUI.BeginDisabledGroup(true);
            foreach (var stat in Stats.All)
                EditorGUILayout.IntField(stat.ToString(), publicStats[stat]);
            EditorGUI.EndDisabledGroup();

            if(!change) return;
            publicStats.Recalculate(member.Level, member, member);
        }
    }
}