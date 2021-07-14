using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ToggleGroup : MonoBehaviour
{
    public List<UI_ToggleButton> allButton;

    public void UI_Start()
    {
        foreach (UI_ToggleButton but in allButton)
        {
            but.UI_Start();
        }
    }


    public void ToggleGroup(UI_ToggleButton button)
    {
        foreach (UI_ToggleButton but in allButton)
        {
            if (but != button)
            {
                but.SetToggleState(false);
            }
            else
            {
                but.SetToggleState(true);
            }
        }
    }
}
