using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellHandler : MonoBehaviour
{
    protected Transform caster;

    protected System.Action<int> consumeMana;

    protected PlayerData refData;

    protected bool isCasting = false;

    protected float castTimer = 0f;
    protected virtual void Update()
    {
        if (isCasting)
        {
            castTimer += Time.deltaTime;
        }
    }
    public virtual void startCasting(Vector3 referencePoint) {
        isCasting = true;
    }
    public virtual void whileCasting(Vector3 referencePoint) { }
    public virtual void onFullCast(Vector3 referencePoint) { }

    public abstract bool finishedCasting();

    public abstract float percentageCompletion();
    public virtual void onDisruptedCast(Vector3 referencePoint) 
    {
        Destroy(gameObject, 0.01f);
    }

    public virtual void setup(Transform _caster, PlayerData data, System.Action<int> cMana)
    {
        consumeMana = cMana;
        caster = _caster;
        refData = data;
    }

    public abstract bool canCastSpell(int currMana);
}
