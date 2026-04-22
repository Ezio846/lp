using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioSource bgmAudio;
    public AudioSource sfxAudio;

    // BGM 音量控制
    public void SetBGMVolume(float value)
    {
        bgmAudio.volume = value;
    }

    // SFX 音量控制
    public void SetSFXVolume(float value)
    {
        sfxAudio.volume = value;
    }
}