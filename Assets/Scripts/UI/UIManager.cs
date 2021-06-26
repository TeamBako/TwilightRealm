using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UI_InGamePanel inGamePanel;
    public UI_UpgradePanel upgradePanel;

    [SerializeField]
    private UI_Panel currentPanel;

    private void Awake()
    {
        Instance = this;
    }


    public void Start()
    {
        SetCurrentPanel(inGamePanel);
    }

    public void Update()
    {
        currentPanel.UI_Update();
    }

    public void ToggleUpgradePanel()
    {
        if(currentPanel == inGamePanel)
        {
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
