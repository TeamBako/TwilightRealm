using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellHandler : MonoBehaviour
{
    private float castTime;
    private float damage;

    public virtual void whileCasting(Vector3 referencePoint) { }
    public virtual void onFullCast(Vector3 referencePoint) { }

    public virtual void onDisruptedCast(Vector3 referencePoint) 
    {
        Destroy(gameObject, 0.01f);
    }
}
