using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDOT : SpellHandler
{
    #region Balance
    [SerializeField]
    private int baseDOT, baseManaConsumption;
    [SerializeField]
    private float baseAOE, baseDuration, baseCastTime;

    [SerializeField]
    private int dotPerLevel;
    [SerializeField]
    private float aoePerLevel, durationPerLevel, castTimeMultiplierPerLevel, manaConsumptionPerLevel;
    #endregion

    [SerializeField]
    private RockAOE rockAOE;
    public override float percentageCompletion()
    {
        float val = castTimer / castTime;
        return val > 1 ? 1 : val;
    }
    // Start is called before the first frame update

    protected float castTime
    {
        get
        {
            return baseCastTime * Mathf.Pow(castTimeMultiplierPerLevel, refData.rockDOTData.castSpeed);
        }
    }

    protected int manaConsumption
    {
        get
        {
            return Mathf.CeilToInt(baseManaConsumption * Mathf.Pow(manaConsumptionPerLevel,
                refData.rockDOTData.manaConsumption));
        }
    }

    protected int damagePerSecond
    {
        get
        {
            return baseDOT + dotPerLevel * refData.rockDOTData.damagePerSecond;
        }
    }

    protected float duration
    {
        get
        {
            return baseDuration + durationPerLevel * refData.rockDOTData.duration;
        }
    }

    protected float areaOfEffect
    {
        get
        {
            return baseAOE + aoePerLevel * refData.rockDOTData.areaOfEffect;
        }
    }

    public override bool finishedCasting()
    {
        return castTimer >= castTime;
    }
    public override void startCasting(Vector3 referencePoint)
    {
        base.startCasting(referencePoint);
        transform.parent = caster;
    }

    public override bool canCastSpell(int currMana)
    {
        return currMana >= manaConsumption;
    }

    public override void onDisruptedCast(Vector3 referencePoint)
    {
        base.onDisruptedCast(referencePoint);
        gameObject.SetActive(false);
        Destroy(gameObject, 10f);
    }

    public override void onFullCast(Vector3 referencePoint)
    {
        base.onFullCast(referencePoint);
        RockAOE aoe = Instantiate(rockAOE, 
            PlayerControl.Instance.transform.position + Vector3.up * 0.1f, rockAOE.transform.rotation);
        aoe.setup(damagePerSecond, areaOfEffect, duration);
        gameObject.SetActive(false);
        Destroy(gameObject, 10f);
        consumeMana(manaConsumption);
    }
}
