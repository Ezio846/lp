using UnityEngine;

public class MainMenuController : MonoBehaviour
{
    [Header("主菜单页面")]
    [SerializeField] private GameObject panelMainMenu;

    [Header("子页面")]
    [SerializeField] private GameObject panelSettings;
    [SerializeField] private GameObject panelSelectSave;
    [SerializeField] private GameObject panelCredits;

    private void Start()
    {
        Debug.Log("MainMenuController：启动，显示主菜单");
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        Debug.Log("MainMenuController：显示主菜单");
        ShowOnlyPanel(panelMainMenu);
    }

    public void ContinueGame()
{
    Debug.Log("MainMenuController：点击 Continue");

    if (GameManager.Instance == null)
    {
        Debug.LogError("MainMenuController：GameManager.Instance 为空");
        return;
    }

    if (SaveManager.Instance == null)
    {
        Debug.LogError("MainMenuController：SaveManager.Instance 为空");
        return;
    }

    int latestSlot = SaveManager.Instance.GetLatestSaveSlot();

    if (latestSlot < 1)
    {
        Debug.Log("MainMenuController：暂无最近存档，打开存档选择页面");
        ShowOnlyPanel(panelSelectSave);
        return;
    }

    if (!SaveManager.Instance.HasSave(latestSlot))
    {
        Debug.Log("MainMenuController：最近槽位没有有效存档，打开存档选择页面");
        ShowOnlyPanel(panelSelectSave);
        return;
    }

    Debug.Log("MainMenuController：继续最近存档，槽位 = " + latestSlot);
    GameManager.Instance.LoadGameFromSlot(latestSlot);
}

    public void StartNewGame()
    {
        Debug.Log("MainMenuController：点击 New Game，直接开始新游戏");

        if (GameManager.Instance == null)
        {
            Debug.LogError("MainMenuController：GameManager.Instance 为空，请先确认主菜单场景里已经放了 GameManager");
            return;
        }

        if (SaveManager.Instance == null)
        {
            Debug.LogError("MainMenuController：SaveManager.Instance 为空，请先确认主菜单场景里已经放了 SaveManager");
            return;
        }

        int defaultNewGameSlot = 1;

        Debug.Log("MainMenuController：使用默认槽位开始新游戏，槽位 = " + defaultNewGameSlot);
        GameManager.Instance.StartGameFromSlot(defaultNewGameSlot, true);
    }

    public void OpenSettings()
    {
        Debug.Log("MainMenuController：打开 Settings");
        ShowOnlyPanel(panelSettings);
    }

    public void OpenSelectSave()
    {
        Debug.Log("MainMenuController：打开 Select Save");
        ShowOnlyPanel(panelSelectSave);
    }

    public void OpenCredits()
    {
        Debug.Log("MainMenuController：打开 Credits");
        ShowOnlyPanel(panelCredits);
    }

    public void BackToMainMenu()
    {
        Debug.Log("MainMenuController：返回主菜单");
        ShowMainMenu();
    }

    public void SelectSaveSlot1()
    {
        Debug.Log("MainMenuController：点击了存档1");
        LoadSlot(1);
    }

    public void SelectSaveSlot2()
    {
        Debug.Log("MainMenuController：点击了存档2");
        LoadSlot(2);
    }

    public void SelectSaveSlot3()
    {
        Debug.Log("MainMenuController：点击了存档3");
        LoadSlot(3);
    }

    private void LoadSlot(int slotIndex)
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("MainMenuController：GameManager.Instance 为空，请先确认主菜单场景里已经放了 GameManager");
            return;
        }

        if (SaveManager.Instance == null)
        {
            Debug.LogError("MainMenuController：SaveManager.Instance 为空，请先确认主菜单场景里已经放了 SaveManager");
            return;
        }

        if (!SaveManager.Instance.HasSave(slotIndex))
        {
            Debug.Log("MainMenuController：槽位 " + slotIndex + " 暂无存档");
            return;
        }

        Debug.Log("MainMenuController：准备读取槽位 " + slotIndex);
        GameManager.Instance.LoadGameFromSlot(slotIndex);
    }

    private void ShowOnlyPanel(GameObject targetPanel)
    {
        if (panelMainMenu != null) panelMainMenu.SetActive(false);
        if (panelSettings != null) panelSettings.SetActive(false);
        if (panelSelectSave != null) panelSelectSave.SetActive(false);
        if (panelCredits != null) panelCredits.SetActive(false);

        if (targetPanel != null)
        {
            targetPanel.SetActive(true);
        }
    }

    public void QuitGame()
    {
        Debug.Log("MainMenuController：点击 Exit，退出游戏");

        Time.timeScale = 1f;

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}