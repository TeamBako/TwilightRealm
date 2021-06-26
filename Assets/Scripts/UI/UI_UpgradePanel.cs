using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradePanel : UI_Panel
{
    [Header("Tab Group")]
    public UI_Tab statUpgrade;
    public UI_Tab fireUpgrade;
    public UI_Tab iceUpgrade;
    public UI_Tab earthUpgrade;

    public UI_Tab currentTab;


    [Header("Stat Group")]
    public UI_UpgradeField hpUp;
    public UI_UpgradeField mpUp;
    public UI_UpgradeField hpRUp;    
    public UI_UpgradeField mpRUp;
    public UI_UpgradeField mvSpdUp;

    [Header("Fire Group")]
    public UI_UpgradeField fireDamage;
    public UI_UpgradeField fireMPCon;
    public UI_UpgradeField fireAOE;
    public UI_UpgradeField fireCastSpeed;
    public UI_UpgradeField fireBurnDamage;
    public UI_UpgradeField fireBurnDuration;

    [Header("Ice Group")]
    public UI_UpgradeField iceDPS;
    public UI_UpgradeField iceMPConR;
    public UI_UpgradeField iceAOC;
    public UI_UpgradeField iceSlowEffect;
    public UI_UpgradeField iceSlowDuration;

    [Header("Earth group")]
    public UI_UpgradeField earthDPS;
    public UI_UpgradeField earthMPCon;
    public UI_UpgradeField earthAOE;
    public UI_UpgradeField earthCastSpeed;
    public UI_UpgradeField earthDuration;

    [Header("ETC")]
    public Text skillPointText;

    private PlayerControl player;

    public override void UI_Start()
    {
        player = PlayerControl.Instance;
        UI_Initialize();
    }

    public void UI_Initialize()
    {
        fireUpgrade.SetTabActive(false);
        iceUpgrade.SetTabActive(false);
        earthUpgrade.SetTabActive(false);

        SelectTab(statUpgrade);
        UpdateSkillPointsInfo();

        UpdateAllStatInfo();
        UpdateStatButtonStatus();

        UpdateAllFireInfo();
        UpdateFireButtonStatus();

        UpdateAllIceInfo();
        UpdateIceButtonStatus();

        UpdateAllEarthInfo();
        UpdateEarthButtonStatus();
    }

    public override void UI_Update()
    {
    }

    public void SelectTab(UI_Tab newTab)
    {
        if(currentTab != newTab)
        {
            if (currentTab != null)
            {
                currentTab.SetTabActive(false);
            }
            currentTab = newTab;

            currentTab.SetTabActive(true);
        }

    }

    public void LevelUpSkillPoints(int number)
    {
        player.PlayerData.availableUpgradePoint += number;
        UpdateSkillPointsInfo();
    }

    public void UpdateSkillPointsInfo()
    {
        skillPointText.text = player.PlayerData.availableUpgradePoint.ToString();
    }

    #region StatUpgradeMethods
    public void UpdateStatButtonStatus()
    {
        bool temp = player.PlayerData.availableUpgradePoint > 0;
        hpUp.SetButtonInteractable(temp);
        mpUp.SetButtonInteractable(temp);
        hpRUp.SetButtonInteractable(temp);
        mpRUp.SetButtonInteractable(temp);
        mvSpdUp.SetButtonInteractable(temp);
    }

    private void UpdateAllStatInfo() 
    { 
        hpUp.UpdateFieldInfo(player.PlayerData.maxHP);
        mpUp.UpdateFieldInfo(player.PlayerData.maxMP);
        hpRUp.UpdateFieldInfo(player.PlayerData.hPRegen);
        mpRUp.UpdateFieldInfo(player.PlayerData.mPRegen);
        mvSpdUp.UpdateFieldInfo(player.PlayerData.movementSpeed);
    }

    public void UpgradePlayerStat(int stUp)
    {
        switch ((StatUpgrade)stUp)
        {
            case StatUpgrade.HP:
                {
                    PlayerControl.Instance.PlayerData.maxHP += 1;
                    break;
                }
            case StatUpgrade.MP:
                {
                    PlayerControl.Instance.PlayerData.maxMP += 1;
                    break;
                }
            case StatUpgrade.HPR:
                {
                    PlayerControl.Instance.PlayerData.hPRegen += 1;
                    break;
                }
            case StatUpgrade.MPR:
                {
                    PlayerControl.Instance.PlayerData.mPRegen += 1;
                    break;
                }
            case StatUpgrade.SPD:
                {
                    PlayerControl.Instance.PlayerData.movementSpeed += 1;
                    break;
                }
        }
        UpdateAllStatInfo();
        LevelUpSkillPoints(-1);
        UpdateStatButtonStatus();
    }

    #endregion

    #region FireUpgradeMethods

    public void UpdateFireButtonStatus()
    {
        bool temp = player.PlayerData.availableUpgradePoint > 0;
        fireDamage.SetButtonInteractable(temp);
        fireMPCon.SetButtonInteractable(temp);
        fireAOE.SetButtonInteractable(temp);
        fireCastSpeed.SetButtonInteractable(temp);
        fireBurnDamage.SetButtonInteractable(temp);
        fireBurnDuration.SetButtonInteractable(temp);
    }

    private void UpdateAllFireInfo()
    {
        fireDamage.UpdateFieldInfo(player.PlayerData.fireBallData.damage);
        fireMPCon.UpdateFieldInfo(player.PlayerData.fireBallData.manaConsumption);
        fireAOE.UpdateFieldInfo(player.PlayerData.fireBallData.areaOfEffect);
        fireCastSpeed.UpdateFieldInfo(player.PlayerData.fireBallData.castSpeed);
        fireBurnDamage.UpdateFieldInfo(player.PlayerData.fireBallData.burnDamage);
        fireBurnDuration.UpdateFieldInfo(player.PlayerData.fireBallData.burnDuration);
    }

    public void UpgradePlayerFire(int fiUp)
    {
        switch ((FireUpgrade)fiUp)
        {
            case FireUpgrade.DMG:
                {
                    PlayerControl.Instance.PlayerData.fireBallData.damage += 1;
                    break;
                }
            case FireUpgrade.MPC:
                {
                    PlayerControl.Instance.PlayerData.fireBallData.manaConsumption += 1;
                    break;
                }
            case FireUpgrade.AOE:
                {
                    PlayerControl.Instance.PlayerData.fireBallData.areaOfEffect += 1;
                    break;
                }
            case FireUpgrade.CAST:
                {
                    PlayerControl.Instance.PlayerData.fireBallData.castSpeed += 1;
                    break;
                }
            case FireUpgrade.BURNDMG:
                {
                    PlayerControl.Instance.PlayerData.fireBallData.burnDamage += 1;
                    break;
                }
            case FireUpgrade.BURNDUR:
                {
                    PlayerControl.Instance.PlayerData.fireBallData.burnDuration += 1;
                    break;
                }
        }
        UpdateAllFireInfo();
        LevelUpSkillPoints(-1);
        UpdateFireButtonStatus();
    }

    #endregion

    #region IceUpgradeMethods

    public void UpdateIceButtonStatus()
    {
        bool temp = player.PlayerData.availableUpgradePoint > 0;
        iceDPS.SetButtonInteractable(temp);
        iceMPConR.SetButtonInteractable(temp);
        iceAOC.SetButtonInteractable(temp);
        iceSlowEffect.SetButtonInteractable(temp);
        iceSlowDuration.SetButtonInteractable(temp);
    }

    private void UpdateAllIceInfo()
    {
        iceDPS.UpdateFieldInfo(player.PlayerData.frostBreathData.damagePerSecond);
        iceMPConR.UpdateFieldInfo(player.PlayerData.frostBreathData.manaConsumptionRate);
        iceAOC.UpdateFieldInfo(player.PlayerData.frostBreathData.areaOfCone);
        iceSlowEffect.UpdateFieldInfo(player.PlayerData.frostBreathData.slowEffect);
        iceSlowDuration.UpdateFieldInfo(player.PlayerData.frostBreathData.slowDuration);
    }

    public void UpgradePlayerIce(int icUp)
    {
        switch ((IceUpgrade)icUp)
        {
            case IceUpgrade.DPS:
                {
                    PlayerControl.Instance.PlayerData.frostBreathData.damagePerSecond += 1;
                    break;
                }
            case IceUpgrade.MPCR:
                {
                    PlayerControl.Instance.PlayerData.frostBreathData.manaConsumptionRate += 1;
                    break;
                }
            case IceUpgrade.AOC:
                {
                    PlayerControl.Instance.PlayerData.frostBreathData.areaOfCone += 1;
                    break;
                }
            case IceUpgrade.SLOWEFF:
                {
                    PlayerControl.Instance.PlayerData.frostBreathData.slowEffect += 1;
                    break;
                }
            case IceUpgrade.SLOWDUR:
                {
                    PlayerControl.Instance.PlayerData.frostBreathData.slowDuration += 1;
                    break;
                }
        }
        UpdateAllIceInfo();
        LevelUpSkillPoints(-1);
        UpdateIceButtonStatus();
    }

    #endregion

    #region EarthUpgradeMethods

    public void UpdateEarthButtonStatus()
    {
        bool temp = player.PlayerData.availableUpgradePoint > 0;
        earthDPS.SetButtonInteractable(temp);
        earthMPCon.SetButtonInteractable(temp);
        earthAOE.SetButtonInteractable(temp);
        earthCastSpeed.SetButtonInteractable(temp);
        earthDuration.SetButtonInteractable(temp);
    }

    private void UpdateAllEarthInfo()
    {
        earthDPS.UpdateFieldInfo(player.PlayerData.rockDOTData.damagePerSecond);
        earthMPCon.UpdateFieldInfo(player.PlayerData.rockDOTData.manaConsumption);
        earthAOE.UpdateFieldInfo(player.PlayerData.rockDOTData.areaOfEffect);
        earthCastSpeed.UpdateFieldInfo(player.PlayerData.rockDOTData.castSpeed);
        earthDuration.UpdateFieldInfo(player.PlayerData.rockDOTData.duration);
    }

    public void UpgradePlayerEarth(int earUp)
    {
        switch ((EarthUpgrade)earUp)
        {
            case EarthUpgrade.DPS:
                {
                    PlayerControl.Instance.PlayerData.rockDOTData.damagePerSecond += 1;
                    break;
                }
            case EarthUpgrade.MPC:
                {
                    PlayerControl.Instance.PlayerData.rockDOTData.manaConsumption += 1;
                    break;
                }
            case EarthUpgrade.AOE:
                {
                    PlayerControl.Instance.PlayerData.rockDOTData.areaOfEffect += 1;
                    break;
                }
            case EarthUpgrade.CAST:
                {
                    PlayerControl.Instance.PlayerData.rockDOTData.castSpeed += 1;
                    break;
                }
            case EarthUpgrade.DURATION:
                {
                    PlayerControl.Instance.PlayerData.rockDOTData.duration += 1;
                    break;
                }
        }
        UpdateAllEarthInfo();
        LevelUpSkillPoints(-1);
        UpdateEarthButtonStatus();
    }

    #endregion
}
public enum StatUpgrade
{
    HP,
    MP,
    HPR,
    MPR,
    SPD,
}

public enum FireUpgrade
{
    DMG,
    MPC,
    AOE,
    CAST,
    BURNDMG,
    BURNDUR,
}

public enum IceUpgrade
{
    DPS,
    MPCR,
    AOC,
    SLOWEFF,
    SLOWDUR,
}

public enum EarthUpgrade
{
    DPS,
    MPC,
    AOE,
    CAST,
    DURATION,
}