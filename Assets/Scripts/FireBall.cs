using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : SpellHandler
{
    #region Balance
    [SerializeField]
    private int baseDamage, baseManaConsumption, baseBurnDamage;
    [SerializeField]
    private float baseAOE, baseBurnDuration, baseCastTime;

    [SerializeField]
    private int damagePerLevel, manaConsumptionPerLevel, burnDamagePerLevel;
    [SerializeField]
    private float aoePerLevel, burnDurationPerLevel, castTimeMultiplierPerLevel;
    #endregion

    public override float percentageCompletion()
    {
        float val = castTimer / castTime;
        return val > 1 ? 1 : val;
    }

    protected float burnDuration
    {
        get
        {
            return baseBurnDuration + burnDurationPerLevel * refData.fireBallData.burnDuration;
        }
    }

    protected int burnEffect
    {
        get
        {
            return baseBurnDamage + burnDamagePerLevel * refData.fireBallData.burnDamage;
        }
    }
    protected float castTime
    {
        get
        {
            return baseCastTime * Mathf.Pow(castTimeMultiplierPerLevel, refData.fireBallData.castSpeed); 
        }
    }

    protected int manaConsumption
    {
        get
        {
            return Mathf.CeilToInt(baseManaConsumption * Mathf.Pow(manaConsumptionPerLevel,
                refData.frostBreathData.manaConsumptionRate));
        }
    }

    protected int damage
    {
        get
        {
            return baseDamage + damagePerLevel * refData.fireBallData.damage;
        }
    }

    protected float areaOfEffect
    {
        get
        {
            return baseAOE + aoePerLevel * refData.fireBallData.areaOfEffect;
        }
    }

    [SerializeField]
    private float speedMultiplier;

    [SerializeField]
    private GameObject explosion;

    public override bool finishedCasting()
    {
        return castTimer >= castTime; 
    }


    public override bool canCastSpell(int currMana)
    {
        return isCasting ? true : currMana >= manaConsumption;
    }

    public override void startCasting(Vector3 referencePoint)
    {
        base.startCasting(referencePoint);
        transform.parent = caster;
    }

    public override void onFullCast(Vector3 target)
    {
        transform.parent = null;
        isCasting = false;
        StartCoroutine(moveTowardsTarget(target));
        consumeMana(manaConsumption);
    }

    protected IEnumerator moveTowardsTarget(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 1)//Changes here
        {
            transform.position += Vector3.Normalize(target - transform.position) * speedMultiplier;
            yield return new WaitForEndOfFrame();
        }
        explode();
    }

    protected void OnTriggerEnter(Collider other)
    {
        //StopAllCoroutines();
        if(!isCasting && (other.gameObject.tag == "Monster" || other.gameObject.tag == "Terrain"))
        {
            explode();
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        //StopAllCoroutines();
        if (!isCasting && (other.gameObject.tag == "Monster" || other.gameObject.tag == "Terrain"))
        {
            explode();
        }
    }

    protected void explode()
    {
        StopAllCoroutines();
        GameObject ex = Instantiate(explosion, transform.position, Quaternion.identity);
        ex.transform.localScale = new Vector3(areaOfEffect, areaOfEffect, areaOfEffect);
        Collider[] cols = Physics.OverlapSphere(transform.position, areaOfEffect * 5);
        
        foreach(Collider c in cols)
        {
            Debug.Log(c.gameObject);
            if (c.tag == "Monster")
            {
                c.GetComponent<AIController>().takeDamage(damage);
                Burn b = c.GetComponent<Burn>();
                if (!b)
                {
                    b = c.gameObject.AddComponent<Burn>();
                }
                b.activate(burnEffect, burnDuration);
            }
        }
        Destroy(ex.gameObject, 2f);
        Destroy(gameObject, 0.01f);
    }
}
