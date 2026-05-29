using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(menuName = "Scriptable/EnemyEncounter", fileName = "New Enemy Encounter")]
    public class Encounter : ScriptableObject {
        public string displayName;
        public List<EnemyInfo> enemies;
    }
}