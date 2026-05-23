using System;
using System.Collections.Generic;
using TurnBasedRPG.Data;

namespace TurnBasedRPG.StatusEffects
{
    public class StatusEffectList {
        private readonly Character _target;
        private readonly Dictionary<StatusSO, Status> _statusList;
        public IReadOnlyDictionary<StatusSO, Status> StatusList => _statusList;
        public event Action<Status> OnStatusAdded;
        public event Action<Status> OnStatusRemoved;

        public StatusEffectList(Character target) {
            _target = target;
            _statusList = new Dictionary<StatusSO, Status>();
        }
    
        public void Apply(StatusSO so) {
            if (_statusList.TryGetValue(so, out var existingStatus)) {
                existingStatus.Stack(_target, so.Status);
            } else {
                var newStatus = so.Clone();
                if (newStatus == null) return;

                if (so.Opposite && _statusList.ContainsKey(so.Opposite))
                    Remove(so.Opposite);
                else
                {
                    _statusList.Add(so, newStatus);
                    newStatus.Apply(_target);
                    OnStatusAdded?.Invoke(newStatus);
                }
            }
        }
    
        public void Remove(StatusSO so)
        {
            if (!_statusList.TryGetValue(so, out var status)) return;
            status.Remove(_target);
            _statusList.Remove(so);
            OnStatusRemoved?.Invoke(status);
        }

        public bool Contain(StatusSO newSo) => _statusList.ContainsKey(newSo);
    }
}