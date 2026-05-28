using System.Collections.Generic;
using TurnBasedRPG.BattleStats;
using UnityEngine;

namespace TurnBasedRPG.Data {
    public interface IPartyMember : IAttributes, IStats {
        string CharName { get; }
        int Level { get; set; }
        Attributes UsedAttributes { get; }
        Profession Profession { get; }
        Element Element { get; }
        IReadOnlyList<IEquipment> Equips { get; }
        IReadOnlyList<ISkill> EquipedSkills { get; }
        IReadOnlyList<ISkill> LearnedSkills { get; }
        Sprite CharacterSprite { get; }
        Sprite UISprite { get; }
        Animator Prefab { get; }
        ISkill BasicAttack { get; }
        int GetUnusedPoints();

        void SetEquip(int index, IEquipment e);
        void SetSkill(int index, ISkill e);
        void LearnSkill(ISkill e);
    }
}