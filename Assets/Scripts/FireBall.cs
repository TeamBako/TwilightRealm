using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : SpellHandler
{
    #region DeterminedData
    private int damage;
    private int manaConsumption;
    private float areaOfEffect;
    private float castSpeed;
    private int burnDamage;
    private float burnDuration;
    #endregion

    [SerializeField]
    private float speedMultiplier;

    [SerializeField]
    private GameObject explosion; //replace with proper explosion calculations

    public override void setup(Transform _caster, PlayerData data, System.Action<int> consumeMana)
    {
        base.setup(_caster, data, consumeMana);
        transform.parent = _caster;
        damage = data.fireBallData.damage;
        manaConsumption = data.fireBallData.manaConsumption;
        areaOfEffect = data.fireBallData.areaOfEffect;
        castSpeed = data.fireBallData.castSpeed;
        burnDamage = data.fireBallData.burnDamage;
        burnDuration = data.fireBallData.burnDuration;
    }

    public override bool canCastSpell(int currMana)
    {
        return currMana >= manaConsumption;
    }

    public override void startCasting(Vector3 referencePoint)
    {
        base.startCasting(referencePoint);
        consumeMana(manaConsumption);
        manaConsumption = 0;
    }

    public override void onFullCast(Vector3 target)
    {
        transform.parent = null;
        StartCoroutine(moveTowardsTarget(target));
    }

    protected IEnumerator moveTowardsTarget(Vector3 target)
    {
        
        while (Vector3.Distance(transform.position, target) > 1)//Changes here
        {
            transform.position += Vector3.Normalize(target - transform.position) * speedMultiplier;
            yield return new WaitForEndOfFrame();
        }
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 2f);
        Destroy(gameObject, 0.01f);
    }

    protected void OnTriggerEnter(Collider other)
    {
        //StopAllCoroutines();
    }
}
