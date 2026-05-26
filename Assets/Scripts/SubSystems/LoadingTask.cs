using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Controllers {
    public class LoadingTask {
        private class Routiner : MonoBehaviour { }

        private readonly Queue<Coroutine> _routines = new();
        private MonoBehaviour _routiner;

        public void Add(IEnumerator ie) {
            if(!_routiner) {
                _routiner = new GameObject("Routiner").AddComponent<Routiner>();
                Object.DontDestroyOnLoad(_routiner.gameObject);
            }

            var r = _routiner.StartCoroutine(ie);
            _routines.Enqueue(r);
        }

        public IEnumerator WaitAll() {
            while (_routines.Count > 0) {
                yield return _routines.Peek();
                _routines.Dequeue();
            }
        }
    }
}