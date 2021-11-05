using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoSingleton<AudioManager>
{
    public static string MIXER_PARAM_MASTER_VOLUME = "Master_Volume";
    public static string MIXER_PARAM_MUSIC_VOLUME = "Music_Volume";
    public static string MIXER_PARAM_SFX_VOLUME = "SFX_Volume";

    public AudioMixer AudioMixer;

    // TODO pool these
    public AudioSource musicEmitter;
    public AudioSource sfxEmitter;

    public void SetMixerVolume(string key, float value)
    {
        AudioMixer.SetFloat(key, NormalizedToMixerValue(value));
    }
    public float GetMixerVolume(string key)
    {
        AudioMixer.GetFloat(key, out var volume);
        return MixerValueToNormalized(volume);
    }

    // Audio Mixer uses dB
    private float MixerValueToNormalized(float mixerValue)
    {
        return 1f + (mixerValue / 80f);
    }

    private float NormalizedToMixerValue(float normalizedValue)
    {
        return (normalizedValue - 1f) * 80f;
    }

    public void PlayMusic(AudioClipSO audioClipSo)
    {
        if (musicEmitter.clip == audioClipSo.audioClip) 
            return;
        musicEmitter.clip = audioClipSo.audioClip;
        // musicEmitter.volume = GetMixerVolume(MIXER_PARAM_MUSIC_VOLUME);
        musicEmitter.Play();
    }

    public void PlaySFX(AudioClipSO audioClipSo)
    {
        sfxEmitter.clip = audioClipSo.audioClip;
        // sfxEmitter.volume = GetMixerVolume(MIXER_PARAM_SFX_VOLUME);;
        sfxEmitter.Play();
    }
}