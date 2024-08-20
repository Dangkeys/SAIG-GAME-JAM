using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsUI : MonoBehaviour
{
    // Start is called before the first frame update
    [field: SerializeField] public Slider sfxVolumeSlider { get; private set; }
    [field: SerializeField] public Slider musicVolumeSlider { get; private set; }
    void Start()
    {
        if (AudioManager.Instance == null) return;
        sfxVolumeSlider.value = AudioManager.Instance.sfxSource.volume;
        musicVolumeSlider.value = AudioManager.Instance.musicSource.volume;
        sfxVolumeSlider.onValueChanged.AddListener(SetSFXVolume);
        musicVolumeSlider.onValueChanged.AddListener(SetMusicVolume);
    }

    private void SetMusicVolume(float volume)
    {
        AudioManager.Instance.SetVolumeBackgroundAndSavedToPlayerPrefs(volume);
    }

    private void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SetVolumeAndSavedToPlayerPrefs(volume);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
