using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UI_InGamePanel inGamePanel;
    public UI_UpgradePanel upgradePanel;
    public UI_DeadPanel deadPanel;
    public UI_PauseMenuPanel pausePanel;

    public RectTransform startWaveButton;
    public RectTransform infoDisplay;
    public float infoDuration;

    [SerializeField]
    private UI_Panel currentPanel;

    private UI_Panel previousPanel;

    [SerializeField]
    private float currentTimer;

    [SerializeField]
    private bool showInfoDisplay;

    private void Awake()
    {
        Instance = this;

        inGamePanel.UI_Awake();
        upgradePanel.UI_Awake();
        deadPanel.UI_Awake();
        pausePanel.UI_Awake();
    }


    public void UI_Start()
    {
        inGamePanel.UI_Start();
        upgradePanel.UI_Start();
        deadPanel.UI_Start();
        pausePanel.UI_Start();
    }

    public void UI_Initialize()
    {
        inGamePanel.UI_Initialize();
        upgradePanel.UI_Initialize();
        deadPanel.UI_Initialize();
        pausePanel.UI_Initialize();

        SetCurrentPanel(inGamePanel);
        showInfoDisplay = false;
        infoDisplay.gameObject.SetActive(false);
    }


    public void Update()
    {
        currentPanel.UI_Update();

        if(showInfoDisplay && currentTimer > 0)
        {
            currentTimer -= Time.unscaledTime * 0.001f; ;
        }
        else if(showInfoDisplay && currentTimer <= 0)
        {
            showInfoDisplay = false;
            infoDisplay.gameObject.SetActive(false);
        }
    }
    
    public void SetMobNoText(int num)
    {
        inGamePanel.UpdateMobNoText(num);
    }

    public void SetWaveStatus(int waveNo)
    {
        inGamePanel.SetWaveStatus(waveNo);
    }

    public void DisplayPlayerDead()
    {
        inGamePanel.UpdateBarInfo();
        SetCurrentPanel(deadPanel);
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

    public void EndOfWaveAction()
    {
        startWaveButton.gameObject.SetActive(true);
        currentTimer = infoDuration;
        infoDisplay.gameObject.SetActive(true);
        showInfoDisplay = true;
        upgradePanel.LevelUpSkillPoints(3);
    }

    #region TempHacks
    public void hackUpgrade()
    {
        upgradePanel.LevelUpSkillPoints(3);
    }

    #endregion
    public void StartWaveAction()
    {
        startWaveButton.gameObject.SetActive(false);

        GameManager.Instance.StartWave();
    }

    public void ToggleUpgradePanel()
    {
        if (currentPanel != pausePanel)
        {
            if (currentPanel == inGamePanel)
            {
                //upgradePanel.UpdateAllInfo();
                SetCurrentPanel(upgradePanel);
                GameManager.Instance.PauseGame();
            }
            else if (currentPanel == upgradePanel)
            {
                SetCurrentPanel(inGamePanel);
                GameManager.Instance.UnpauseGame();
            }
        }
    }

    public void ToggleEscapePanel()
    {
        if (currentPanel != pausePanel)
        {
            //upgradePanel.UpdateAllInfo();
            SetCurrentPanel(pausePanel);
            GameManager.Instance.PauseGame();
        }
        else
        {
            if (previousPanel != upgradePanel)
            {
                GameManager.Instance.UnpauseGame();
            }

            SetCurrentPanel(previousPanel);
        }
    }

    public void SetCurrentPanel(UI_Panel newPanel)
    {
        if(currentPanel != null)
        {
            if (newPanel == pausePanel && currentPanel == upgradePanel)
            {

            }
            else
            {
                currentPanel.TogglePanel(false);
            }
            previousPanel = currentPanel;
        }

        currentPanel = newPanel;

        currentPanel.TogglePanel(true);
    }

    public void SetSkillHighlight(SpellType spellType)
    {
        inGamePanel.SetSkillDisplay(spellType);
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

public enum Transition
{
    NONE,
    INSTANT,
}
