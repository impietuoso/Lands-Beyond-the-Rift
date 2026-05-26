using System.Collections.Specialized;
using System.Linq;
using TurnBasedRPG.BattleStats;
using UnityEngine;

namespace RPG {
    public class MemberStatsView : DataView<PartyMember> {
        [SerializeField] private DeltaStatView[] stats;
        [SerializeField] private DataView compareEquip;

        protected override void Subscribe() {
            compareEquip = null;
            Refresh();
            Data.UsedAttributes.OnChanged += RefreshOnAttribute;
            ((ObservableList<Equipment>)Data.Equips).CollectionChanged += RefreshOnEquip;
        }

        protected override void Unsubscribe() {
            compareEquip = null;
            Data.UsedAttributes.OnChanged -= RefreshOnAttribute;
            ((ObservableList<Equipment>)Data.Equips).CollectionChanged -= RefreshOnEquip;
        }

        private void SetCompareEquip(DataView view) {
            compareEquip = view;
            Refresh();
        }

        private void RefreshOnAttribute(Attribute _) => Refresh();
        private void RefreshOnEquip(object sender, NotifyCollectionChangedEventArgs e) => Refresh();

        public void Refresh() {
            var equipB = compareEquip.GetData() as Equipment;
            var equipA = equipB ? Data.Equips.FirstOrDefault(e => e.Type == equipB.Type) ?? Stats.Zero : Stats.Zero;

            foreach (var view in stats) {
                var s = view.Stat;
                var v = Data[s];
                var d = equipB ? equipA[s] - equipB[s] : 0;
                view.SetData((v, d));
            }
        }
    }
}