using UnityEngine;
using UnityEngine.EventSystems;

public class UIButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{
    [Header("悬停音效")]
    public AudioClip hoverSound;

    [Header("悬停音效设置")]
    public float cooldown = 0.05f;

    [Range(0f, 1f)]
    public float hoverVolume = 0.4f;

    private float lastPlayTime = -999f;

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("鼠标进入按钮: " + gameObject.name);
        TryPlayHover();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("鼠标点击按钮: " + gameObject.name);
    }

    private void TryPlayHover()
    {
        if (Time.unscaledTime - lastPlayTime < cooldown)
            return;

        lastPlayTime = Time.unscaledTime;

        if (AudioManager.Instance != null && hoverSound != null)
        {
            AudioManager.Instance.PlaySFX(hoverSound, hoverVolume);
        }
        else
        {
            Debug.LogWarning("AudioManager.Instance 或 hoverSound 为空");
        }
    }
}