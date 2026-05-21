using UnityEngine;
public class PersistenteConfig : MonoBehaviour {
    public static PersistenteConfig Instance;
    public LoadController load;
    public AudioManager audio;
    public ResolutionController resolution;
    public CanvasGroup optionsPanel;
    private void Awake() {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        } else {
            Destroy(gameObject);
        }
    }

    public void ToggleOptions(bool toggle) {
        optionsPanel.blocksRaycasts = toggle;
        optionsPanel.alpha = toggle ? 1 : 0;
    }
}