using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HighscoreField : MonoBehaviour
{
    public Text pName;
    public Text wave;

    public void Update_Field(Highscore_Info info)
    {
        pName.text = info.name;
        wave.text = "Wave: " + info.wave_no.ToString();
    }
}

[System.Serializable]
public class Highscore_Info
{
    public string name;
    public int wave_no;

    public Highscore_Info(string name, int wave_no)
    {
        this.name = name;
        this.wave_no = wave_no;
    }


}
