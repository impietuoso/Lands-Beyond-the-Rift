using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Controllers {
    public class LoadingTask {
        private class Routiner : MonoBehaviour { }

        private readonly HashSet<Coroutine> _routines = new();
        private MonoBehaviour _routiner;

        public void Add(IEnumerator routine) {
            StartRoutine(routine);
        }

        public void StartRoutine(IEnumerator ie) {
            if(!_routiner) {
                _routiner = new GameObject("Routiner").AddComponent<Routiner>();
                Object.DontDestroyOnLoad(_routiner.gameObject);
            }

            var r = _routiner.StartCoroutine(ie);
            _routines.Add(r);
        }
    }
}