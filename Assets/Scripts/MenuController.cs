using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour {
    public void StartGame() {
        PersistenteConfig.Instance.load.ChangeScene(1);
    }
    
    public void OpenOptions() {
        PersistenteConfig.Instance.ToggleOptions(true);
    }
}