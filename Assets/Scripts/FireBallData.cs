using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FireBallData
{
    public int damage;
    public int manaConsumption;
    public int areaOfEffect;
    public int castSpeed;
    public int burnDamage;
    public int burnDuration;

    public FireBallData()
    {
        damage = 0;
        manaConsumption = 0;
        areaOfEffect = 0;
        castSpeed = 0;
        burnDamage = 0;
        burnDuration = 0;
    }
}
