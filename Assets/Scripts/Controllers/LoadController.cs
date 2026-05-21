using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadController : MonoBehaviour {
    public Animator loadscreen;
    public float loadDelay;
    public void ChangeScene(int sceneId) {
        Fade();
        StartCoroutine(Loading(sceneId));
    }

    public IEnumerator Loading(int sceneId) {
        yield return new WaitForSeconds(loadDelay);
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        operation.completed += Fade;
    }

    private void Fade(AsyncOperation obj) {
        Fade();
    }

    public void Fade() {
        loadscreen.SetTrigger("Fade");
    }
}
