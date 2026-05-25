using System;
using System.Collections.Generic;
using TurnBasedRPG.Data;
using TurnBasedRPG.UI.Combat;

namespace TurnBasedRPG {
    public class CombatConfig {
        public CombatArena Arena;
        public IEnumerable<IPartyMember> Allies;
        public IEnumerable<IPartyMember> Enemies;
        public IEnumerable<(IConsumable, int)> Items;
        public Action<bool> Callback;
    }
}