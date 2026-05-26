using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG {
    public class WeaponView : DataView<Weapon> {
        [field: SerializeField] public TMP_Text Name { get; private set; }
        [field: SerializeField] public TMP_Text Description { get; private set; }
        [field: SerializeField] public Image Icon { get; private set; }

        protected override void Subscribe() {
            if (Name) Name.text = Data.name;
            if (Description) Description.text = Data.description;
            if (Icon) Icon.overrideSprite = Data.sprite;
        }

        protected override void Unsubscribe() { }
    }
}