using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int maxHP;
    public int maxMP;
    //and all the other points
    public int mPRegen;
    public int hPRegen;
    public int movementSpeed;

    public FireBallData fireBallData;
    public FrostBreathData frostBreathData;
    public RockDOTData rockDOTData;
    public PlayerData()
    {
        maxHP = 0;
        maxMP = 0;
        mPRegen = 0;
        hPRegen = 0;
        movementSpeed = 0;
        fireBallData = new FireBallData();
        frostBreathData = new FrostBreathData();
        rockDOTData = new RockDOTData();
        //and all the other points
    }
}
