using System.Collections.Specialized;
using System.Linq;
using Data;
using TurnBasedRPG.BattleStats;
using UnityEngine;

namespace UI {
    public class MemberStatsView : DataView<PartyMember> {
        [SerializeField] private DeltaStatView[] stats;
        [SerializeField] private DataView compareEquip;
        private readonly Stats _statsPreview = new();

        protected override void Subscribe() {
            compareEquip.SetData(null);
            Refresh();
            Data.UsedAttributes.OnChanged += RefreshOnAttribute;
            ((ObservableList<Equipment>)Data.Equips).CollectionChanged += RefreshOnEquip;
        }

        protected override void Unsubscribe() {
            compareEquip.SetData(null);
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
            _statsPreview.Recalculate(Data.Level, Data, Data);
            var equipB = compareEquip ? compareEquip.GetData() as Equipment : null;
            var equipA = equipB ? Data.Equips.FirstOrDefault(e => e?.Type == equipB.Type) ?? Stats.Zero : Stats.Zero;

            foreach (var view in stats) {
                var s = view.Stat;
                var v = _statsPreview[s];
                var d = equipB ? equipB[s] - equipA[s] : 0;
                view.SetData((v, d));
            }
        }
    }
}