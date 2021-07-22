using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_HighscorePanel : UI_Panel
{
    public List<UI_HighscoreField> allHighscoreField;
    public GameObject noHighscore;

    private HighscoreData hsData;

    public override void UI_Start()
    {
        base.UI_Start();
        TogglePanel(false);
    }

    public override void UI_Initialize()
    {
        base.UI_Initialize();

        hsData = GameManager.Instance.HS_Deactivate();
        UpdateAllField();
    }

    public void UpdateAllField()
    {
        noHighscore.SetActive(hsData.highscore_Arr == null || hsData.highscore_Arr.Length == 0);

        for (int i = 0; i < 5; i++)
        {
            if (hsData.highscore_Arr != null && i < hsData.highscore_Arr.Length)
            {
                allHighscoreField[i].gameObject.SetActive(true);
                allHighscoreField[i].Update_Field(hsData.highscore_Arr[i]);
            }
            else
            {
                allHighscoreField[i].gameObject.SetActive(false);
            }
        }
    }

    public override void UI_Update()
    {
        base.UI_Update();
    }
}

[System.Serializable]
public class HighscoreData
{
    public Highscore_Info[] highscore_Arr;

    public HighscoreData()
    {
        highscore_Arr = new Highscore_Info[0];
    }
}