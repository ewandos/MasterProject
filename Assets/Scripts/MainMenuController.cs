using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public GameObject loadingUI;
    
    public void LoadMainScene()
    {
        loadingUI.SetActive(true);
        Invoke(nameof(LoadGameScene), 0.5f);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadSceneAsync("Main");
    }

    public void LoadSettingsScene()
    {
        SceneManager.LoadScene("Settings");
    }

    public void ExitApplication()
    {
        Application.Quit();
    }
}
