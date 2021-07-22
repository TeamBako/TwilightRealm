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
        noHighscore.SetActive(hsData.highscore_Arr == null || hsData.highscore_Arr.Count == 0);

        for (int i = 0; i < 5; i++)
        {
            if (hsData.highscore_Arr != null && i < hsData.highscore_Arr.Count)
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
    public List<Highscore_Info> highscore_Arr = new List<Highscore_Info>();

    public HighscoreData()
    {
        highscore_Arr = new List<Highscore_Info>();
    }

    public void AddHighscore(Highscore_Info new_Info)
    {
        if(highscore_Arr == null)
        {
            highscore_Arr = new List<Highscore_Info>();
        }

        highscore_Arr.Add(new_Info);

        highscore_Arr.Sort(new HighscoreComparer());
        
        if(highscore_Arr.Count > 5)
        {
            highscore_Arr.RemoveAt(highscore_Arr.Count - 1);
        }
    }
}

public class HighscoreComparer : IComparer<Highscore_Info> 
{
    public int Compare(Highscore_Info x, Highscore_Info y)
    {
        if (x.wave_no > y.wave_no)
        {
            return -1;
        }
        else if (x.wave_no < y.wave_no)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}