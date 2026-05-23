using TurnBasedRPG.DrawerHelpers;
using TurnBasedRPG.StatusEffects;
using UnityEngine;

// ReSharper disable once InconsistentNaming
namespace TurnBasedRPG.Data {
    [CreateAssetMenu(menuName = "Scriptable/Status", fileName = "New Status")]
    public class StatusSO : ScriptableObject {
        [SerializeField] private Sprite icon;
        [SerializeField] private string displayName;
        [SerializeField] private string description;
        [SerializeField] private StatusType type;
        [SerializeField] private Color textColor;
        [SerializeField] private StatusSO opposite;
        [SerializeReference, TypeInstance] private Status status;

        public Sprite Icon => icon;
        public string DisplayName => displayName;
        public string Description => description;
        public StatusType Type => type;
        public Color TextColor => textColor;
        public StatusSO Opposite => opposite;
        public Status Status => status;

        public Status Clone() {
            var clone = Instantiate(this).Status;
            clone.Source = this;
            return clone;
        }
    }
}