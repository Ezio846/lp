using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("音源")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("默认UI点击音效")]
    public AudioClip buttonClickClip;

    [Header("当前场景滑条")]
    public Slider bgmSlider;
    public Slider sfxSlider;

    private const string BGM_KEY = "BGMVolume";
    private const string SFX_KEY = "SFXVolume";

    private float currentBGMVolume;
    private float currentSFXVolume;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(transform.root.gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (bgmSource != null)
        {
            bgmSource.loop = true;
            bgmSource.playOnAwake = true;
            bgmSource.spatialBlend = 0f;
        }

        if (sfxSource != null)
        {
            sfxSource.playOnAwake = false;
            sfxSource.loop = false;
            sfxSource.spatialBlend = 0f;
            sfxSource.ignoreListenerPause = true;
        }

        currentBGMVolume = PlayerPrefs.GetFloat(BGM_KEY, 0.4f);
        currentSFXVolume = PlayerPrefs.GetFloat(SFX_KEY, 0.4f);

        ApplyVolume();
    }

    private void ApplyVolume()
    {
        if (bgmSource != null)
            bgmSource.volume = currentBGMVolume;

        if (sfxSource != null)
            sfxSource.volume = currentSFXVolume;
    }

    public void SetBGMVolume(float value)
    {
        currentBGMVolume = value;

        if (bgmSource != null)
            bgmSource.volume = value;

        PlayerPrefs.SetFloat(BGM_KEY, value);
        PlayerPrefs.Save();
    }

    public void SetSFXVolume(float value)
    {
        currentSFXVolume = value;

        if (sfxSource != null)
            sfxSource.volume = value;

        PlayerPrefs.SetFloat(SFX_KEY, value);
        PlayerPrefs.Save();
    }

    public void PlaySFX(AudioClip clip)
    {
        PlaySFX(clip, 1f);
    }

    public void PlaySFX(AudioClip clip, float volumeScale)
    {
        if (sfxSource == null || clip == null) return;
        sfxSource.PlayOneShot(clip, volumeScale);
    }

    public void PlayButtonClick()
    {
        if (buttonClickClip == null) return;
        PlaySFX(buttonClickClip);
    }

    // 关键：给当前场景的滑条绑定到 AudioManager
    public void BindSliders(Slider bgm, Slider sfx)
    {
        bgmSlider = bgm;
        sfxSlider = sfx;

        if (bgmSlider != null)
        {
            bgmSlider.onValueChanged.RemoveListener(SetBGMVolume);
            bgmSlider.value = currentBGMVolume;
            bgmSlider.onValueChanged.AddListener(SetBGMVolume);
        }

        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.RemoveListener(SetSFXVolume);
            sfxSlider.value = currentSFXVolume;
            sfxSlider.onValueChanged.AddListener(SetSFXVolume);
        }
    }
}