using UnityEngine;

public class MenuController : MonoBehaviour {
    public void StartGame() => Game.Loading.ChangeScene(1);
    public void OpenOptions() => Game.Options.SetActive(true);
    public void CloseGame() => Application.Quit();
}