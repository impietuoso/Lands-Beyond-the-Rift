using System;
using TMPro;

namespace TurnBasedRPG.UI.Views
{
    [Obsolete]
    public class EnemyEncounterView : DataView<object> {
        public TextMeshProUGUI encounterName;
        public ListView enemyList;

        protected override void Subscribe() {
            throw new NotImplementedException();
        }

        protected override void Unsubscribe() {
            throw new NotImplementedException();
        }
    }
}
