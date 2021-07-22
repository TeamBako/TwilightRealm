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

    public int CompareOther(Highscore_Info other)
    {
        if(other.wave_no > this.wave_no)
        {
            return 1;
        }
        else if(other.wave_no < this.wave_no)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }
}
