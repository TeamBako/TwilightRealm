using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_OptionMenu : MonoBehaviour
{
    public UI_ToggleButton gameOptionButton;
    public UI_ToggleButton autoWaveButton;

    public void SetAutoWaveStatus(UI_ToggleButton button)
    {
        GameManager.Instance.SetAutoWaveStatus(button.isToggled);
    }
    public void UI_Start()
    {
        gameOptionButton.UI_Start();
        autoWaveButton.UI_Start();
    }

    public void UI_Initialize()
    {
        gameOptionButton.isToggled = false;
        gameOptionButton.SetToggleState(false);
        autoWaveButton.SetToggleState(GameManager.Instance.autoWaveStart);
    }
}
