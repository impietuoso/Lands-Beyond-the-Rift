using System;
using System.Collections.Generic;
using UnityEngine;

namespace Explorations {
    public class ExplorationTask : MonoBehaviour {
        private static readonly HashSet<object> _tasks = new();
        public static bool IsBusy => _tasks.Count > 0;
        public static event Action<bool> OnBusyChanged;

        private void OnEnable() => Add(this);
        private void OnDisable() => Remove(this);

        public static void Add(object task) {
            if(_tasks.Add(task) && _tasks.Count == 1)
                OnBusyChanged?.Invoke(true);
        }

        public static void Remove(object task) {
            if(_tasks.Remove(task) && _tasks.Count == 0)
                OnBusyChanged?.Invoke(false);
        }
    }
}