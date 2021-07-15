using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MenuManager : MonoBehaviour
{
    public static UI_MenuManager Instance;

    public UI_MainPanel mainPanel;
    public UI_PauseMenuPanel pausePanel;


    [SerializeField]
    private UI_Panel currentPanel;

    private UI_Panel previousPanel;

    private void Awake()
    {
        Instance = this;

        mainPanel.UI_Awake();
        pausePanel.UI_Awake();
    }


    public void UI_Start()
    {
        mainPanel.UI_Start();
        pausePanel.UI_Start();
    }

    public void UI_Initialize()
    {
        mainPanel.UI_Initialize();
        pausePanel.UI_Initialize();

        SetCurrentPanel(mainPanel);
    }


    public void Update()
    {
        currentPanel.UI_Update();
    }

    public void SetCurrentPanel(UI_Panel newPanel)
    {
        if (currentPanel != null)
        {
            currentPanel.TogglePanel(false);
            previousPanel = currentPanel;
        }

        currentPanel = newPanel;

        currentPanel.TogglePanel(true);
    }

    public void ResetGame()
    {
        SystemManager.Instance.ResetPlayerData();
        ChangeScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ChangeScene(int sceneNo)
    {
        SceneManager.LoadScene(sceneNo);
    }

    public void SaveGameSettings()
    {
        SystemManager.Instance.saveGameSettings();
    }

    public Vector3 GetSoundSettings()
    {
        return pausePanel.GetSoundSettings();
    }

    public void LoadSoundSettings(Vector3 settings)
    {
        pausePanel.LoadSoundSettings(settings);
    }

    public void QuitGame()
    {
        SystemManager.Instance.QuitGame();
    }
}
