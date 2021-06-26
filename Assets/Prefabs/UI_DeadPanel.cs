using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DeadPanel : UI_Panel
{
    public override void UI_Start()
    {
        base.UI_Start();
        TogglePanel(false);
    }

    public override void UI_Initialize()
    {
        base.UI_Initialize();
    }

}
