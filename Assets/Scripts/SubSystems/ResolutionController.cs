using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Controllers {
    public class ResolutionController : MonoBehaviour {
        public TMP_Dropdown resolutionDropdown;
        public TMP_Dropdown windowModeDropdown;
        Resolution[] resolutions;
        List<Resolution> filteredResoltions;
        double currentRefreshRate;
    
        private void Start() {
            CreateResolutionList();
            SetResolutionValueInUI();
        }
    
        public void CreateResolutionList() {
            resolutions = Screen.resolutions;
            filteredResoltions = new List<Resolution>();
            resolutionDropdown.ClearOptions();

            currentRefreshRate = Screen.currentResolution.refreshRateRatio.value;

            for (int i = 0; i < resolutions.Length; i++) {
                if (resolutions[i].refreshRateRatio.value == currentRefreshRate) {
                    filteredResoltions.Add(resolutions[i]);
                }
            }

            List<string> options = new();
            foreach (var item in filteredResoltions) {
                string resolutionOption = $"Resolução: {item.width} x {item.height}";
                options.Add(resolutionOption);
            }

            resolutionDropdown.AddOptions(options);
            resolutionDropdown.RefreshShownValue();
        }
    
        public void SetResolutionValueInUI() {
            if (PlayerPrefs.GetInt("Resolution", 0) >= filteredResoltions.Count)
                PlayerPrefs.SetInt("Resolution", filteredResoltions.Count - 1);
        
            resolutionDropdown.value = PlayerPrefs.GetInt("Resolution", filteredResoltions.Count - 1);
            windowModeDropdown.value = PlayerPrefs.GetInt("WindowMode", 0);

            SetResolution(PlayerPrefs.GetInt("Resolution", 0));
        }

        public void SetResolution(int id) {
            Resolution resolution = filteredResoltions[id];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode);
            PlayerPrefs.SetInt("Resolution", id);
        }

        public void ToggleFullScreen(int id) {
            var fullscreen = id == 0 ? true : false;
            Screen.fullScreen = fullscreen;
            PlayerPrefs.SetInt("FullScreen", id);
        }
    }
}