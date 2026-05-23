using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Controllers {
    public class LoadController : MonoBehaviour {
        public Animator loadscreen;
        public float loadDelay;

        public Coroutine ChangeScene(int sceneId) {
            return StartCoroutine(_ChangeScene(sceneId));
        }

        private IEnumerator _ChangeScene(int sceneId) {
            loadscreen.SetTrigger("Fade");
            yield return new WaitForSeconds(loadDelay);
            var op = SceneManager.LoadSceneAsync(sceneId);
            op!.completed += Fade;
        }

        private void Fade(AsyncOperation obj) {
            loadscreen.SetTrigger("Fade");
        }
    }
}