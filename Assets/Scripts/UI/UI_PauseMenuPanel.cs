using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PauseMenuPanel : UI_Panel
{
    public UI_OptionMenu optionMenu;

    public override void UI_Start()
    {
        base.UI_Start();
        TogglePanel(false);
        optionMenu.UI_Start();
    }

    public override void UI_Initialize()
    {
        base.UI_Initialize();
        optionMenu.UI_Initialize();
    }

    public void ToggleOptionGroup()
    {
        optionMenu.gameObject.SetActive(!optionMenu.gameObject.activeSelf);
    }

    public Vector3 GetSoundSettings()
    {
        return optionMenu.GetVolumeMixer();
    }

    public void LoadSoundSettings(Vector3 settings)
    {
        optionMenu.SetVolumeMixer(settings);
    }
}
