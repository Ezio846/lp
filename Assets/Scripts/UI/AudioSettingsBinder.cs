using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsBinder : MonoBehaviour
{
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider sfxSlider;

    private void OnEnable()
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.BindSliders(bgmSlider, sfxSlider);
        }
    }
}