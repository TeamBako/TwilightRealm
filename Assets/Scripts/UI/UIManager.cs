using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    public UI_InGamePanel inGamePanel;

    private UI_Panel currentPanel;

    private void Awake()
    {
        Instance = this;
    }


    public void Start()
    {
        currentPanel = inGamePanel;
  
    }

    public void Update()
    {
        currentPanel.UI_Update();
    }

    public void SetCurrentPanel(UI_Panel newPanel)
    {
        currentPanel = newPanel;
    }

    public void SetSkillHighlight(SpellType spellType)
    {
        inGamePanel.SetSkillDisplay(spellType);
    }
}
