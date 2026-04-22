using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadDemo()
    {
        Debug.Log("切换到 Scene_Demo");
        SceneManager.LoadScene("Scene_Demo");
    }

    public void LoadMainMenu()
    {
        Debug.Log("切换到 Scene_MainMenu");
        SceneManager.LoadScene("Scene_MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("退出游戏");

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}