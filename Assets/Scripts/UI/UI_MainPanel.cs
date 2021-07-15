using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MainPanel : UI_Panel
{
    public GameObject continueButton;

    public override void UI_Start()
    {
        base.UI_Start();
    }

    public override void UI_Initialize()
    {
        base.UI_Initialize();
        continueButton.SetActive(GameManager.Instance.deactivate().waveNo > 1);


    }
}
