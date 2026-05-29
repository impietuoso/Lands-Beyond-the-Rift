using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SubSystems {
    public class LoadController : MonoBehaviour {
        public Animator loadscreen;
        public float loadDelay;

        public void ChangeScene(int sceneId) {
            StartCoroutine(_ChangeScene(sceneId));
        }

        private IEnumerator _ChangeScene(int sceneId) {
            loadscreen.SetTrigger("Fade");
            yield return new WaitForSeconds(loadDelay);

            var op = SceneManager.LoadSceneAsync(sceneId);
            yield return op;
            yield return Game.LoadTasks.WaitAll();
            
            yield return new WaitForSeconds(loadDelay);
            loadscreen.SetTrigger("Fade");
        }
    }
}