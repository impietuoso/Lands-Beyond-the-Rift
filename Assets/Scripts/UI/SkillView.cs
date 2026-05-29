using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class SkillView : DataView<Skill> {
        [field: SerializeField] public TMP_Text Name { get; private set; }
        [field: SerializeField] public TMP_Text Description { get; private set; }
        [field: SerializeField] public Image Icon { get; private set; }
        [field: SerializeField] public Image Element { get; private set; }
        [field: SerializeField] public TMP_Text Cost { get; private set; }

        protected override void Subscribe() {
            if(Name) Name.text = Data.name;
            if(Description) Description.text = Data.Description;
            if(Icon) {
                Icon.color = Color.white;
                Icon.overrideSprite = Data.Icon;
            }

            if(Element) Element.overrideSprite = Data.Element.elementSprite;
            if(Cost) Cost.text = Data.Cost.ToString();
        }

        protected override void Unsubscribe() {
            if (Icon) {
                Icon.overrideSprite = null;
                Icon.color = new(1, 1, 1, .15f);
            }
        }
    }
}