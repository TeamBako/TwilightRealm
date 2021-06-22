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
            int curr = baseManaConsumption - manaConsumptionPerLevel * refData.fireBallData.manaConsumption;
            return curr >= 0 ? curr : 0;
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
    private Explosion explosion; //replace with proper explosion calculations

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
            Debug.Log(other.gameObject);
            explode();
        }
    }

    protected void explode()
    {
        StopAllCoroutines();
        Explosion ex = Instantiate(explosion, transform.position, Quaternion.identity);
        ex.setDamage(damage);
        ex.gameObject.transform.localScale = new Vector3(areaOfEffect, areaOfEffect, areaOfEffect);
        Destroy(ex.gameObject, 2f);
        Destroy(gameObject, 0.01f);
    }
}
