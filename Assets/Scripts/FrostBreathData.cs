using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FrostBreathData
{
    public int damagePerSecond;
    public int manaConsumptionRate;
    public int areaOfCone;
    public int slowEffect;
    public int slowDuration;
    public FrostBreathData()
    {
        damagePerSecond = 0;
        manaConsumptionRate = 0;
        areaOfCone = 0;
        slowEffect = 0;//divisor multiplier of enemy speed
        slowDuration = 0;
    }
}
