using System;
using System.Collections.Generic;
using TurnBasedRPG.DrawerHelpers;
using UnityEngine;

namespace RPG {
    [Serializable, CreateAssetMenu(menuName = "Scriptable/EnemyInfo")]
    public class EnemyInfo : PartyMember {
        [SerializeField] private Drop[] drops;
        public IReadOnlyList<Drop> Drops => drops;

        [Serializable]
        public class Drop : ISingleLineDrawer {
            [field: SerializeField] public Item Item { get; private set; }
            [field: SerializeField, Range(0, 1)] public float Chance { get; private set; }
        }
    }
}