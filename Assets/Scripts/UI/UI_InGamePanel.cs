using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_InGamePanel : UI_Panel
{
    public UI_Bar hp_Bar;
    public Text hp_Text;
    public UI_Bar mp_Bar;
    public Text mp_Text;

    private PlayerControl player;

    public override void UI_Start()
    {
        player = PlayerControl.Instance;
        UI_Initialize();
    }

    public void UI_Initialize()
    {
        hp_Bar.UI_Initialize();
        mp_Bar.UI_Initialize();

        UpdateBarInfo();
    }

    public void UpdateBarInfo()
    {
        hp_Text.text = player.getCurrentHP() + "/" + player.getMaxHP();
        mp_Text.text = player.getCurrentMP() + "/" + player.getMaxMP();

        hp_Bar.SetUIPercentage(player.getCurrentHP() / (float) player.getMaxHP());
        mp_Bar.SetUIPercentage(player.getCurrentMP() / (float) player.getMaxMP());

        hp_Bar.UI_Update();
        mp_Bar.UI_Update();
    }

    public override void UI_Update()
    {
        UpdateBarInfo();
        
    }

}
