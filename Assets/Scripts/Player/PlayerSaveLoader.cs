using System.Collections;
using UnityEngine;

public class PlayerSaveLoader : MonoBehaviour
{
    private IEnumerator Start()
    {
        // 等一帧，先让场景里的出生点/初始化逻辑先跑完
        yield return null;

        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.TryApplyLoadedPlayerPosition(transform);
            Debug.Log("[PlayerSaveLoader] 已尝试恢复玩家位置");
        }
        else
        {
            Debug.LogWarning("[PlayerSaveLoader] SaveManager.Instance 为空");
        }
    }
}