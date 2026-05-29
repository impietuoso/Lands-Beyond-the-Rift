using System.Collections.Generic;
using TurnBasedRPG.BattleStats;
using TurnBasedRPG.Skills;
using UnityEngine;

namespace TurnBasedRPG.Data {
    public interface IPartyMember : IAttributes, IStats {
        string CharName { get; }
        int Level { get; set; }
        int CurrentHp { get; set; }
        int CurrentMp { get; set; }
        Attributes UsedAttributes { get; }
        Element Element { get; }
        IReadOnlyList<IEquipment> Equips { get; }
        IReadOnlyList<ISkill> EquipedSkills { get; }
        IReadOnlyList<ISkill> LearnedSkills { get; }
        Sprite CharacterSprite { get; }
        Sprite UISprite { get; }
        Animator Prefab { get; }
        ISkill BasicAttack { get; }
        int GetUnusedPoints();
        
        IEnumerable<IPassive> GetPassives();
        IEnumerable<ISkill> GetSkills();

        void SetEquip(int index, IEquipment e);
        void SetSkill(int index, ISkill e);
        void LearnSkill(ISkill e);
    }
}