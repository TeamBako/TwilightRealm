using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_DeadPanel : UI_Panel
{
    public GameObject saveArea;
    public GameObject buttonArea;

    public Text inputName;

    public override void UI_Start()
    {
        base.UI_Start();
        TogglePanel(false);
    }

    public override void UI_Initialize()
    {
        base.UI_Initialize();
        saveArea.SetActive(true);
        buttonArea.SetActive(false);
    }

    public void SaveGame()
    {
        saveArea.SetActive(false);
        buttonArea.SetActive(true);

        GameManager.Instance.SaveHS(inputName.text == "" || inputName.text == "Input Name..." ? "Player" : inputName.text);
    }
}
