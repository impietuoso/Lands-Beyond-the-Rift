using System.Collections.Generic;
using UnityEngine;

namespace TurnBasedRPG.Data
{
    [CreateAssetMenu(menuName = "Scriptable/EnemyEncounter", fileName = "New Enemy Encounter")]
    public class EnemyEncounter : ScriptableObject {
        public string encounterName;
        public List<IPartyMember> enemyList;
    }
}