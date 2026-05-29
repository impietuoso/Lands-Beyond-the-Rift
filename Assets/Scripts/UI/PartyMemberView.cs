using Data;
using TMPro;
using TurnBasedRPG.BattleStats;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class PartyMemberView : DataView<PartyMember> {
        [field: SerializeField] public TMP_Text CharName { get; private set; }
        [field: SerializeField] public TMP_Text Level { get; private set; }
        [field: SerializeField] public TMP_Text Profession { get; private set; }
        [field: SerializeField] public Image Element { get; private set; }
        [field: SerializeField] public ListView Attributes { get; private set; }
        [field: SerializeField] public MemberStatsView Stats { get; private set; }
        [field: SerializeField] public FixedListView Equips { get; private set; }
        [field: SerializeField] public ListView Consumables { get; private set; }
        [field: SerializeField] public FixedListView EquipedSkills { get; private set; }
        [field: SerializeField] public ListView LearnedSkills { get; private set; }
        [field: SerializeField] public Image Portrait { get; private set; }

        protected override void Subscribe() {
            if(CharName) CharName.text = Data.CharName;
            if(Level) Level.text = "Lv. " + Data.Level;
            if(Profession) Profession.text = Data.Profession.name;
            if(Element) Element.overrideSprite = Data.Element.elementSprite;
            if(Attributes) Attributes.SetData(((IAttributes)Data).Pairs());
            if(Stats) Stats.SetData(Data);
            if(Equips) Equips.SetData(Data.Equips);
            if(Consumables) Consumables.SetData(Data.Consumables);
            if(EquipedSkills) EquipedSkills.SetData(Data.EquipedSkills);
            if(LearnedSkills) LearnedSkills.SetData(Data.LearnedSkills);
            if(Portrait) Portrait.overrideSprite = Data.UISprite;
        }

        protected override void Unsubscribe() { }
    }
}