using System;
using TurnBasedRPG.DrawerHelpers;
using Attribute = TurnBasedRPG.BattleStats.Attribute;

namespace TurnBasedRPG.Skills.SkillEffects {
    [Serializable]
    public class AttributeScale : ISingleLine2Drawer {
        public Attribute attribute;
        public float scale = 1f;
        public int GetValue(Character c) => (int)(c[attribute] * scale);
    }
}