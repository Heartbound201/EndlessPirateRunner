using UnityEngine.Events;
using UnityEngine.UI;

public class SettingsView : View
{
    public UnityAction OnMenuClicked;

    public AudioManager audioManager;
    
    public Slider musicSlider;
    public Slider sfxSlider;

    private void Start()
    {
        musicSlider.onValueChanged.AddListener(
            delegate
            {
                // SavePref(PREF_KEY_MUSIC_VOLUME, musicSlider.value);
                audioManager.SetMixerVolume(AudioManager.MIXER_PARAM_MUSIC_VOLUME, musicSlider.value);
            });
        sfxSlider.onValueChanged.AddListener(
            delegate
            {
                // SavePref(PREF_KEY_SFX_VOLUME, sfxSlider.value);
                audioManager.SetMixerVolume(AudioManager.MIXER_PARAM_SFX_VOLUME, sfxSlider.value);
            });
    }

    private void LoadPrefs()
    {
        musicSlider.value = audioManager.GetMixerVolume(AudioManager.MIXER_PARAM_MUSIC_VOLUME);
        sfxSlider.value = audioManager.GetMixerVolume(AudioManager.MIXER_PARAM_SFX_VOLUME);
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