using UnityEngine;
using TMPro;

public class SaveSlotUI : MonoBehaviour
{
    [SerializeField] private int slotIndex = 1;

    [Header("文字引用")]
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private TMP_Text line1Text;
    [SerializeField] private TMP_Text line2Text;

    private void OnEnable()
    {
        RefreshUI();
    }

    public void RefreshUI()
    {
        if (titleText == null || line1Text == null || line2Text == null)
        {
            Debug.LogWarning("[SaveSlotUI] 文字引用没有绑完整");
            return;
        }

        bool isCurrentSlot = false;
        if (GameManager.Instance != null)
        {
            isCurrentSlot = (GameManager.Instance.CurrentSaveSlot == slotIndex);
        }

        titleText.text = isCurrentSlot ? $"Slot {slotIndex}【当前】" : $"Slot {slotIndex}";

        if (SaveManager.Instance == null)
        {
            line1Text.text = "SaveManager 未找到";
            line2Text.text = "";
            return;
        }

        SaveData data = SaveManager.Instance.GetSaveData(slotIndex);

        if (data == null)
        {
            line1Text.text = "空存档";
            line2Text.text = "";
        }
        else
        {
            line1Text.text = data.sceneName;
            line2Text.text = data.saveTime;
        }
    }
}