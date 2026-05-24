using System;
using System.Collections.Generic;
using TurnBasedRPG.Data;
using TurnBasedRPG.UI.Combat;

namespace TurnBasedRPG {
    public class CombatConfig {
        public CombatArena Arena;
        public IEnumerable<PartyMember> Allies;
        public IEnumerable<PartyMember> Enemies;
        public List<Slot<Item>> Items;
        public Action<bool> Callback;
    }
}