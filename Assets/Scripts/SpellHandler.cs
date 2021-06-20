using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellHandler : MonoBehaviour
{
    protected Transform caster;

    protected System.Action<int> consumeMana;

    //[SerializeField]
    //private bool useOwnRotation;

    ////public bool ownRotation()
    ////{
    ////    return useOwnRotation;
    ////}

    public virtual void startCasting(Vector3 referencePoint) { }
    public virtual void whileCasting(Vector3 referencePoint) { }
    public virtual void onFullCast(Vector3 referencePoint) { }

    public virtual void onDisruptedCast(Vector3 referencePoint) 
    {
        Destroy(gameObject, 0.01f);
    }

    public virtual void setup(Transform _caster, PlayerData data, System.Action<int> cMana)
    {
        consumeMana = cMana;
        caster = _caster;
    }

    public abstract bool canCastSpell(int currMana);
}
