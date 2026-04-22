using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    MainMenu,
    Loading,
    Playing,
    Paused,
    Dialogue
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("场景名")]
    [SerializeField] private string mainMenuSceneName = "Scene_MainMenu";
    [SerializeField] private string demoSceneName = "Scene_Demo";

    public GameState CurrentState { get; private set; } = GameState.MainMenu;
    public int CurrentSaveSlot { get; private set; } = -1;
    public bool IsNewGame { get; private set; } = true;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        Debug.Log("[GameManager] 启动成功");
        UpdateStateByScene(SceneManager.GetActiveScene().name);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("[GameManager] 场景已加载: " + scene.name);
        UpdateStateByScene(scene.name);
    }

    private void UpdateStateByScene(string sceneName)
    {
        if (sceneName == mainMenuSceneName)
        {
            ChangeState(GameState.MainMenu);
        }
        else if (sceneName == demoSceneName)
        {
            ChangeState(GameState.Playing);
        }
    }

    public void StartGameFromSlot(int slotIndex, bool isNewGame)
    {
        CurrentSaveSlot = slotIndex;
        IsNewGame = isNewGame;

        if (SaveManager.Instance != null)
        {
            SaveManager.Instance.ClearPendingLoad();
        }

        Debug.Log("[GameManager] 开始进入游戏, 槽位 = " + slotIndex + ", IsNewGame = " + isNewGame);

        Time.timeScale = 1f;
        ChangeState(GameState.Loading);
        SceneManager.LoadScene(demoSceneName);
    }

    public void LoadGameFromSlot(int slotIndex)
    {
        if (SaveManager.Instance == null)
        {
            Debug.LogError("[GameManager] SaveManager.Instance 为空");
            return;
        }

        SaveData data = SaveManager.Instance.GetSaveData(slotIndex);

        if (data == null)
        {
            Debug.LogWarning("[GameManager] 槽位 " + slotIndex + " 没有存档，无法加载");
            return;
        }

        CurrentSaveSlot = slotIndex;
        IsNewGame = false;

        SaveManager.Instance.PrepareLoad(slotIndex);

        Debug.Log("[GameManager] 准备读取存档, 槽位 = " + slotIndex + ", 场景 = " + data.sceneName);

        Time.timeScale = 1f;
        ChangeState(GameState.Loading);
        SceneManager.LoadScene(data.sceneName);
    }

    public void ReturnToMainMenu()
    {
        Debug.Log("[GameManager] 返回主菜单");

        Time.timeScale = 1f;
        ChangeState(GameState.Loading);
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void PauseGame()
    {
        if (CurrentState != GameState.Playing)
            return;

        Time.timeScale = 0f;
        ChangeState(GameState.Paused);
        Debug.Log("[GameManager] 游戏暂停");
    }

    public void ResumeGame()
    {
        if (CurrentState != GameState.Paused)
            return;

        Time.timeScale = 1f;
        ChangeState(GameState.Playing);
        Debug.Log("[GameManager] 游戏继续");
    }

    public void EnterDialogue()
    {
        if (CurrentState != GameState.Playing)
            return;

        ChangeState(GameState.Dialogue);
        Debug.Log("[GameManager] 进入对话状态");
    }

    public void ExitDialogue()
    {
        if (CurrentState != GameState.Dialogue)
            return;

        ChangeState(GameState.Playing);
        Debug.Log("[GameManager] 退出对话状态");
    }

    private void ChangeState(GameState newState)
    {
        if (CurrentState == newState)
            return;

        Debug.Log("[GameManager] 状态切换: " + CurrentState + " -> " + newState);
        CurrentState = newState;
    }
    public void SetCurrentSaveSlot(int slotIndex)
{
    CurrentSaveSlot = slotIndex;
    Debug.Log("[GameManager] 当前槽位已切换为: " + slotIndex);
}
public bool AutoSaveCurrentSlot()
{
    if (SaveManager.Instance == null)
    {
        Debug.LogError("[GameManager] SaveManager.Instance 为空，自动保存失败");
        return false;
    }

    if (CurrentSaveSlot < 1)
    {
        Debug.LogWarning("[GameManager] 当前槽位无效，自动保存失败");
        return false;
    }

    SaveManager.Instance.SaveGame(CurrentSaveSlot);
    Debug.Log("[GameManager] 已自动保存当前槽位：" + CurrentSaveSlot);
    return true;
}
}