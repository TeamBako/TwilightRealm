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

    public UI_Skill fire_Skill;
    public UI_Skill ice_Skill;
    public UI_Skill earth_Skill;

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

        fire_Skill.UI_Initialize();
        ice_Skill.UI_Initialize();
        earth_Skill.UI_Initialize();

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

    public void SetSkillDisplay(SpellType spellType)
    {
        switch (spellType)
        {
            case SpellType.FIRE:
                {
                    fire_Skill.UI_SetActive(true);
                    ice_Skill.UI_SetActive(false);
                    earth_Skill.UI_SetActive(false);
                    break;
                }
            case SpellType.ICE:
                {
                    fire_Skill.UI_SetActive(false);
                    ice_Skill.UI_SetActive(true);
                    earth_Skill.UI_SetActive(false);
                    break;
                }
            case SpellType.EARTH:
                {
                    fire_Skill.UI_SetActive(false);
                    ice_Skill.UI_SetActive(false);
                    earth_Skill.UI_SetActive(true);
                    break;
                }
        }

    }

    public void UpdateSkillInfo()
    {
        if(fire_Skill.UI_GetActive())
        {
            fire_Skill.UI_Update();
        }
        else if (ice_Skill.UI_GetActive())
        {
            ice_Skill.UI_Update();
        }
        else if (earth_Skill.UI_GetActive())
        {
            earth_Skill.UI_Update();
        }
    }

    public override void UI_Update()
    {
        UpdateBarInfo();
        UpdateSkillInfo();
    }


}
public enum SpellType
{
    FIRE,
    ICE,
    EARTH
}