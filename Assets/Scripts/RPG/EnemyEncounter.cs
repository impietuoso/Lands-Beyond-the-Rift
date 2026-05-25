using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    [CreateAssetMenu(menuName = "Scriptable/EnemyEncounter", fileName = "New Enemy Encounter")]
    public class Encounter : ScriptableObject {
        public string displayName;
        public List<EnemyInfo> enemies;
    }
}