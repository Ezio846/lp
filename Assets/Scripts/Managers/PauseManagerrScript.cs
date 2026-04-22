using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
            else
                ResumeGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0f;

        // 🔥 关键：暂停所有声音（不管有几个AudioSource）
        AudioListener.pause = true;

        isPaused = true;
        Debug.Log("游戏暂停");
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;

        // 🔥 恢复所有声音
        AudioListener.pause = false;

        isPaused = false;
        Debug.Log("游戏继续");
    }
}