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

    public UI_Bar casting_Bar;
    public Sprite fill_Img;
    public Sprite load_Img;

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
        casting_Bar.UI_Initialize();
        casting_Bar.gameObject.SetActive(false);

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
        

        if(player.spellCasted)
        {
            if (!casting_Bar.gameObject.activeSelf)
            {
                casting_Bar.gameObject.SetActive(true);
            }

            if(player.spellCasted.percentageCompletion() == 1)
            {
                casting_Bar.fill.GetComponent<Image>().sprite = fill_Img;
            }

            casting_Bar.SetUIPercentage(player.spellCasted.percentageCompletion());
            casting_Bar.UI_Update();
        } 
        else if(casting_Bar.gameObject.activeSelf)
        {
            casting_Bar.gameObject.SetActive(false);
            casting_Bar.SetUIPercentage(0);
            casting_Bar.fill.GetComponent<Image>().sprite = load_Img;
        }
    }

    public override void UI_Update()
    {
        UpdateBarInfo();
        
    }

}
