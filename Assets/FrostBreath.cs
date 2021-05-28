using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBreath : SpellHandler
{
    // Start is called before the first frame update
    public override void whileCasting(Vector3 referencePoint)
    {
        base.whileCasting(referencePoint);
        //dps dps
        transform.LookAt(referencePoint);
    }

    public override void onFullCast(Vector3 referencePoint)
    {
        base.onFullCast(referencePoint);
        Destroy(gameObject, 0.1f);
    }
}
