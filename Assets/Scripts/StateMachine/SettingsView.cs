using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsView : View
{
    public UnityAction OnMenuClicked;

    public static string PREF_KEY_MUSIC_VOLUME = "music_volume";
    public static string PREF_KEY_SFX_VOLUME = "sfx_volume";

    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        musicSlider.onValueChanged.AddListener(
            delegate
            {
                SavePref(PREF_KEY_MUSIC_VOLUME, musicSlider.value);
                // TODO play sample sound for feedback
            });
        sfxSlider.onValueChanged.AddListener(
            delegate
            {
                SavePref(PREF_KEY_SFX_VOLUME, sfxSlider.value);
                // TODO play sample sound for feedback
            });
    }

    private void SavePref(string key, float value)
    {
        PlayerPrefs.SetFloat(key, value);
        PlayerPrefs.Save();
        
    }

    private void LoadPrefs()
    {
        musicSlider.value = PlayerPrefs.GetFloat(PREF_KEY_MUSIC_VOLUME, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(PREF_KEY_SFX_VOLUME, 1f);
    }


    public override void Show()
    {
        base.Show();
        LoadPrefs();
    }

    public void MenuClick()
    {
        OnMenuClicked?.Invoke();
    }
}