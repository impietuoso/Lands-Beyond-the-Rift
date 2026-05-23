using System;
using System.Collections.Generic;

namespace TurnBasedRPG.BattleStats {
    [Serializable]
    public class BoolStat {
        private readonly HashSet<object> _on = new ();
        public void Add(object src) => _on.Add(src);
        public void Remove(object src) => _on.Remove(src);
        public static implicit operator bool(BoolStat s) => s._on.Count > 0;
    }
}