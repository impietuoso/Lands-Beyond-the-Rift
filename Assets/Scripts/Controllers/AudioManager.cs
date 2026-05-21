using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    [Header("Audio Mixers")]
    public AudioMixer mixer;
    public AudioSource bgmAudioSource;
    public AudioSource sfxAudioSource;

    [Header("Sliders")]
    public Slider masterSlider;
    public Slider bgmSlider;
    public Slider sfxSlider;
    
    [Header("Toggles")]
    public Toggle masterToggle;
    public Toggle bgmToggle;
    public Toggle sfxToggle;

    [Header("Texts")]
    public TextMeshProUGUI masterText;
    public TextMeshProUGUI bgmText;
    public TextMeshProUGUI sfxText;

    private void Start() {
        LoadVolumes();
    }

    public void PlayAudio(AudioClip clip, bool loop = false) {
        if (bgmAudioSource == null || clip == null) return;
        if (bgmAudioSource.isPlaying && bgmAudioSource.clip == clip) return;

        bgmAudioSource.clip = clip;
        bgmAudioSource.loop = loop;
        bgmAudioSource.Play();
    }

    public void StopAudio() {
        if (bgmAudioSource.isPlaying) {
            bgmAudioSource.Stop();
        }
    }

    public void PlaySFX(AudioClip clip) {
        if (sfxAudioSource == null || clip == null) return;

        sfxAudioSource.PlayOneShot(clip);
    }

    public void ChangeMasterVolume(float volume) {
        masterText.text = "Master - " + (volume*100).ToString("F0");

        mixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("Master", volume);
        if(volume > 0 && !masterToggle.isOn) masterToggle.SetIsOnWithoutNotify(true);
    }

    public void MuteMaster(bool sound) {
        if (sound) {
            mixer.SetFloat("Master", Mathf.Log10(masterSlider.value) * 20);
        } else {
            mixer.SetFloat("Master", -80f);
        }
    }

    public void ChangeBGMVolume(float volume) {
        bgmText.text = "BGM - " + (volume * 100).ToString("F0");

        mixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("BGM", volume);
        if(volume > 0 && !bgmToggle.isOn) bgmToggle.SetIsOnWithoutNotify(true);
    }

    public void MuteBGM(bool sound) {
        if (sound) {
            mixer.SetFloat("BGM", Mathf.Log10(bgmSlider.value) * 20);
        } else {
            mixer.SetFloat("BGM", -80f);
        }
    }

    public void ChangeSFXVolume(float volume) {
        sfxText.text = "SFX - " + (volume * 100).ToString("F0");

        mixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("SFX", volume);
        if(volume > 0 && !sfxToggle.isOn) sfxToggle.SetIsOnWithoutNotify(true);
    }

    public void MuteSFX(bool sound) {
        if (sound) {
            mixer.SetFloat("SFX", Mathf.Log10(sfxSlider.value) * 20);
        } else {
            mixer.SetFloat("SFX", -80f);
        }
    }

    public void LoadVolumes() {
        masterSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("Master", 0.5f));
        ChangeMasterVolume(masterSlider.value);
        bgmSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("BGM", 0.5f));
        ChangeBGMVolume(bgmSlider.value);
        sfxSlider.SetValueWithoutNotify(PlayerPrefs.GetFloat("SFX", 0.5f));
        ChangeSFXVolume(sfxSlider.value);
    }
}