using UnityEngine;

public class PauseMenuController : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenuPanel;
    [SerializeField] private GameObject pauseMainBox;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject selectSavePanel;

    private bool wasBgmPlayingBeforePause = false;

    private void Start()
    {
        HideAllPauseUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance == null)
            {
                Debug.LogError("PauseMenuController：GameManager.Instance 为空");
                return;
            }

            if (GameManager.Instance.CurrentState == GameState.Playing)
            {
                PauseGame();
            }
            else if (GameManager.Instance.CurrentState == GameState.Paused)
            {
                if (settingsPanel != null && settingsPanel.activeSelf)
                {
                    CloseSettings();
                }
                else if (selectSavePanel != null && selectSavePanel.activeSelf)
                {
                    CloseSelectSave();
                }
                else
                {
                    ResumeGame();
                }
            }
        }
    }

    public void PauseGame()
    {
        if (GameManager.Instance == null) return;

        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(true);
        if (pauseMainBox != null) pauseMainBox.SetActive(true);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (selectSavePanel != null) selectSavePanel.SetActive(false);

        GameManager.Instance.PauseGame();

        if (AudioManager.Instance != null && AudioManager.Instance.bgmSource != null)
        {
            wasBgmPlayingBeforePause = AudioManager.Instance.bgmSource.isPlaying;
            AudioManager.Instance.bgmSource.Pause();
            Debug.Log("PauseGame：已暂停BGM");
        }
        else
        {
            Debug.LogWarning("PauseGame：找不到 AudioManager 或 bgmSource");
        }

        Debug.Log("PauseMenuController：打开暂停菜单");
    }

    public void ResumeGame()
    {
        if (GameManager.Instance == null) return;

        HideAllPauseUI();
        GameManager.Instance.ResumeGame();

        AudioListener.pause = false;

        if (AudioManager.Instance != null && AudioManager.Instance.bgmSource != null)
        {
            AudioSource bgm = AudioManager.Instance.bgmSource;

            if (wasBgmPlayingBeforePause)
            {
                bgm.UnPause();

                if (!bgm.isPlaying && bgm.clip != null)
                {
                    bgm.Play();
                    Debug.Log("ResumeGame：UnPause无效，已强制 Play BGM");
                }
                else
                {
                    Debug.Log("ResumeGame：已恢复BGM");
                }
            }
        }
        else
        {
            Debug.LogWarning("ResumeGame：找不到 AudioManager 或 bgmSource");
        }

        Debug.Log("PauseMenuController：恢复游戏");
    }

   public void ReturnToMainMenu()
{
    if (GameManager.Instance == null) return;

    // 先自动保存
    GameManager.Instance.AutoSaveCurrentSlot();

    HideAllPauseUI();
    AudioListener.pause = false;

    if (AudioManager.Instance != null && AudioManager.Instance.bgmSource != null)
    {
        AudioSource bgm = AudioManager.Instance.bgmSource;
        bgm.UnPause();

        if (!bgm.isPlaying && bgm.clip != null)
        {
            bgm.Play();
        }
    }

    GameManager.Instance.ReturnToMainMenu();
    Debug.Log("PauseMenuController：返回主菜单前已自动保存");
}

    public void QuitGame()
{
    if (GameManager.Instance != null)
    {
        GameManager.Instance.AutoSaveCurrentSlot();
    }

    Time.timeScale = 1f;
    AudioListener.pause = false;

    Debug.Log("PauseMenuController：退出游戏前已自动保存");

#if UNITY_EDITOR
    UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
}

    public void SaveGame()
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("PauseMenuController：GameManager.Instance 为空");
            return;
        }

        if (SaveManager.Instance == null)
        {
            Debug.LogError("PauseMenuController：SaveManager.Instance 为空");
            return;
        }

        int slotIndex = GameManager.Instance.CurrentSaveSlot;

        if (slotIndex < 1)
        {
            Debug.LogWarning("PauseMenuController：当前槽位无效，无法保存");
            return;
        }

        SaveManager.Instance.SaveGame(slotIndex);
        Debug.Log("PauseMenuController：已保存到当前槽位 " + slotIndex);
    }

    public void OpenSelectSave()
    {
        if (pauseMainBox != null) pauseMainBox.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (selectSavePanel != null) selectSavePanel.SetActive(true);

        Debug.Log("打开 Select Save");
    }

    public void CloseSelectSave()
    {
        if (selectSavePanel != null) selectSavePanel.SetActive(false);
        if (pauseMainBox != null) pauseMainBox.SetActive(true);

        Debug.Log("返回 Pause Menu");
    }

    public void OpenSettings()
    {
        if (pauseMainBox != null) pauseMainBox.SetActive(false);
        if (selectSavePanel != null) selectSavePanel.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(true);

        Debug.Log("打开 Settings");
    }

    public void CloseSettings()
    {
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (pauseMainBox != null) pauseMainBox.SetActive(true);

        Debug.Log("返回 Pause Menu");
    }

    public void SelectSaveSlot1()
    {
        SaveToSlot(1);
    }

    public void SelectSaveSlot2()
    {
        SaveToSlot(2);
    }

    public void SelectSaveSlot3()
    {
        SaveToSlot(3);
    }

    private void SaveToSlot(int slotIndex)
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("PauseMenuController：GameManager.Instance 为空");
            return;
        }

        if (SaveManager.Instance == null)
        {
            Debug.LogError("PauseMenuController：SaveManager.Instance 为空");
            return;
        }

        GameManager.Instance.SetCurrentSaveSlot(slotIndex);
        SaveManager.Instance.SaveGame(slotIndex);

        Debug.Log("PauseMenuController：已手动保存到槽位 " + slotIndex);

        if (selectSavePanel != null) selectSavePanel.SetActive(false);
        if (pauseMainBox != null) pauseMainBox.SetActive(true);
    }

    private void HideAllPauseUI()
    {
        if (pauseMenuPanel != null) pauseMenuPanel.SetActive(false);
        if (pauseMainBox != null) pauseMainBox.SetActive(false);
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (selectSavePanel != null) selectSavePanel.SetActive(false);
    }
}