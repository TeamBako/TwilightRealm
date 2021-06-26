using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UI_InGamePanel inGamePanel;
    public UI_UpgradePanel upgradePanel;

    public RectTransform startWaveButton;
    public RectTransform infoDisplay;
    public float infoDuration;

    [SerializeField]
    private UI_Panel currentPanel;

    [SerializeField]
    private float currentTimer;

    [SerializeField]
    private bool showInfoDisplay;

    private void Awake()
    {
        Instance = this;

        inGamePanel.UI_Awake();
        upgradePanel.UI_Awake();
    }


    public void Start()
    {
        inGamePanel.UI_Start();
        upgradePanel.UI_Start();
    }

    public void UI_Initialize()
    {
        inGamePanel.UI_Initialize();
        upgradePanel.UI_Initialize();
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

    public void EndOfWaveAction()
    {
        startWaveButton.gameObject.SetActive(true);
        currentTimer = infoDuration;
        infoDisplay.gameObject.SetActive(true);
        showInfoDisplay = true;
        upgradePanel.LevelUpSkillPoints(1);
    }

    public void StartWaveAction()
    {
        startWaveButton.gameObject.SetActive(false);

        GameManager.Instance.StartWave();
    }

    public void ToggleUpgradePanel()
    {
        if(currentPanel == inGamePanel)
        {
            //upgradePanel.UpdateAllInfo();
            SetCurrentPanel(upgradePanel);
            GameManager.Instance.TogglePauseGame();
        }
        else if(currentPanel == upgradePanel)
        {
            SetCurrentPanel(inGamePanel);
            GameManager.Instance.TogglePauseGame();
        }
    }

    public void SetCurrentPanel(UI_Panel newPanel)
    {
        if(currentPanel != null)
        {
            currentPanel.TogglePanel(false);
        }

        currentPanel = newPanel;

        currentPanel.TogglePanel(true);
    }

    public void SetSkillHighlight(SpellType spellType)
    {
        inGamePanel.SetSkillDisplay(spellType);
    }


}

public enum Transition
{
    NONE,
    INSTANT,
}
