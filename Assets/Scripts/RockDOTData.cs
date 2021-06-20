using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class RockDOTData
{
    public int damagePerSecond;
    public int manaConsumption;
    public int areaOfEffect;
    public int castSpeed;
    public int duration;

    public RockDOTData()
    {
        damagePerSecond = 0;
        manaConsumption = 0;
        areaOfEffect = 0;
        castSpeed = 0;
        duration = 0;
    }
}
