using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBreath : SpellHandler
{
    #region DeterminedData
    private int damagePerSecond;
    private int manaConsumptionRate;
    private float areaOfCone;
    private float slowEffect;
    private float slowDuration;
    #endregion

    public override float percentageCompletion()
    {
        return 1;
    }
    public override bool finishedCasting()
    {
        return true;
    }
    public override bool canCastSpell(int currMana)
    {
        return currMana >= manaConsumptionRate;
    }

    // Start is called before the first frame update
    public override void whileCasting(Vector3 referencePoint)
    {
        base.whileCasting(referencePoint);
        //dps dps
        Vector3 pos = referencePoint;
        pos.y = transform.position.y;
        transform.LookAt(pos);
        consumeMana(manaConsumptionRate);
    }


    public override void onFullCast(Vector3 referencePoint)
    {
        base.onFullCast(referencePoint);
        transform.parent = null;
        Destroy(gameObject, 0.5f);
    }
}
