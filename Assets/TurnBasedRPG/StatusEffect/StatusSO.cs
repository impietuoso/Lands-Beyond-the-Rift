using UnityEngine;

// ReSharper disable once InconsistentNaming
namespace TurnBasedRPG.StatusEffect
{
    [CreateAssetMenu(menuName = "Scriptable/Status", fileName = "New Status")]
    public class StatusSO : ScriptableObject {
        public Sprite statusIcon;
        [SerializeReference, TypeDropdown(typeof(Status))]
        public Status status;
        public StatusType statusType;
        public Color statusPopupColor;

        public Status Clone() {
            var clone = Instantiate(this).status;
            clone.source = this;
            return clone;
        }
    }
}