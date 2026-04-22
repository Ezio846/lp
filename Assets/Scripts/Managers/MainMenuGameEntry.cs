using UnityEngine;

public class MainMenuGameEntry : MonoBehaviour
{
    [Header("面板")]
    [SerializeField] private GameObject panelMainMenu;
    [SerializeField] private GameObject panelSelectSave;

    private bool nextActionIsNewGame = true;

    public void ClickNewGame()
    {
        nextActionIsNewGame = true;

        if (panelMainMenu != null) panelMainMenu.SetActive(false);
        if (panelSelectSave != null) panelSelectSave.SetActive(true);

        Debug.Log("[MainMenuGameEntry] 点击了 New Game");
    }

    public void ClickContinue()
    {
        nextActionIsNewGame = false;

        if (panelMainMenu != null) panelMainMenu.SetActive(false);
        if (panelSelectSave != null) panelSelectSave.SetActive(true);

        Debug.Log("[MainMenuGameEntry] 点击了 Continue");
    }

    public void SelectSlot_1()
    {
        EnterGameWithSlot(1);
    }

    public void SelectSlot_2()
    {
        EnterGameWithSlot(2);
    }

    public void SelectSlot_3()
    {
        EnterGameWithSlot(3);
    }

    public void BackToMainMenuPanel()
    {
        if (panelSelectSave != null) panelSelectSave.SetActive(false);
        if (panelMainMenu != null) panelMainMenu.SetActive(true);

        Debug.Log("[MainMenuGameEntry] 返回主菜单面板");
    }

    private void EnterGameWithSlot(int slotIndex)
    {
        if (GameManager.Instance == null)
        {
            Debug.LogError("[MainMenuGameEntry] GameManager.Instance 为空！");
            return;
        }

        GameManager.Instance.StartGameFromSlot(slotIndex, nextActionIsNewGame);
    }
}