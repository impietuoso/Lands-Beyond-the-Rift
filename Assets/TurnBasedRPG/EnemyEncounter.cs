using System.Collections.Generic;
using TurnBasedRPG.Data;
using UnityEngine;

namespace TurnBasedRPG
{
    [CreateAssetMenu(menuName = "Scriptable/EnemyEncounter", fileName = "New Enemy Encounter")]
    public class EnemyEncounter : ScriptableObject {
        public string encounterName;
        public List<PartyMember> enemyList;
    }
}