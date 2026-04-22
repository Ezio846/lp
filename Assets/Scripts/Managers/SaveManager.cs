using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private const string LatestSaveSlotKey = "LATEST_SAVE_SLOT";

    public SaveData PendingLoadData { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private string GetSlotKey(int slotIndex)
    {
        return "SAVE_SLOT_" + slotIndex;
    }

    public bool HasSave(int slotIndex)
    {
        return PlayerPrefs.HasKey(GetSlotKey(slotIndex));
    }

    public SaveData GetSaveData(int slotIndex)
    {
        if (!HasSave(slotIndex))
        {
            return null;
        }

        string json = PlayerPrefs.GetString(GetSlotKey(slotIndex));
        return JsonUtility.FromJson<SaveData>(json);
    }

    public int GetLatestSaveSlot()
    {
        return PlayerPrefs.GetInt(LatestSaveSlotKey, -1);
    }

    public void SaveGame(int slotIndex)
    {
        SaveData data = new SaveData();

        data.slotIndex = slotIndex;
        data.sceneName = SceneManager.GetActiveScene().name;
        data.isNewGame = GameManager.Instance != null && GameManager.Instance.IsNewGame;
        data.saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Vector3 pos = player.transform.position;
            data.playerX = pos.x;
            data.playerY = pos.y;
            data.playerZ = pos.z;
        }
        else
        {
            Debug.LogWarning("[SaveManager] 没有找到带 Player Tag 的对象，位置将保存为默认值");
        }

        string json = JsonUtility.ToJson(data);

        PlayerPrefs.SetString(GetSlotKey(slotIndex), json);
        PlayerPrefs.SetInt(LatestSaveSlotKey, slotIndex);
        PlayerPrefs.Save();

        Debug.Log("[SaveManager] 已保存到槽位 " + slotIndex + "，时间：" + data.saveTime);
    }

    public void PrepareLoad(int slotIndex)
    {
        SaveData data = GetSaveData(slotIndex);

        if (data == null)
        {
            Debug.LogWarning("[SaveManager] 槽位 " + slotIndex + " 没有存档，无法加载");
            return;
        }

        PendingLoadData = data;

        PlayerPrefs.SetInt(LatestSaveSlotKey, slotIndex);
        PlayerPrefs.Save();

        Debug.Log("[SaveManager] 已准备加载槽位 " + slotIndex);
    }

    public void ClearPendingLoad()
    {
        PendingLoadData = null;
    }

    public void TryApplyLoadedPlayerPosition(Transform playerTransform)
    {
        if (PendingLoadData == null)
        {
            return;
        }

        Vector3 loadedPos = new Vector3(
            PendingLoadData.playerX,
            PendingLoadData.playerY,
            PendingLoadData.playerZ
        );

        playerTransform.position = loadedPos;

        Debug.Log("[SaveManager] 已恢复玩家位置到：" + loadedPos);

        ClearPendingLoad();
    }

    public void DeleteSave(int slotIndex)
    {
        string key = GetSlotKey(slotIndex);

        if (PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.DeleteKey(key);
            PlayerPrefs.Save();
            Debug.Log("[SaveManager] 已删除槽位 " + slotIndex);
        }

        if (GetLatestSaveSlot() == slotIndex)
        {
            PlayerPrefs.DeleteKey(LatestSaveSlotKey);
            PlayerPrefs.Save();
        }
    }
}