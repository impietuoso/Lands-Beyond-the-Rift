using System;
using System.Collections.Generic;
using UnityEngine;

namespace RPG {
    [Serializable, CreateAssetMenu(menuName = "Scriptable/EnemyInfo")]
    public class EnemyInfo : PartyMember {
        [SerializeField] private Item[] drops;
        public IReadOnlyList<Item> Drops => drops;
    }
}