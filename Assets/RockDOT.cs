using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockDOT : SpellHandler
{
    #region DeterminedData
    private int damagePerSecond;
    private int manaConsumption;
    private float areaOfEffect;
    private float castSpeed;
    private float duration;
    #endregion

    // Start is called before the first frame update
    public override void setup(Transform _caster, PlayerData data, System.Action<int> consumeMana)
    {
        base.setup(_caster, data, consumeMana);
        transform.position = transform.position + Vector3.down * 1.5f;
        damagePerSecond = data.rockDOTData.damagePerSecond;
        manaConsumption = data.rockDOTData.manaConsumption;
        areaOfEffect = data.rockDOTData.areaOfEffect;
        castSpeed = data.rockDOTData.castSpeed;
        duration = data.rockDOTData.duration;
    }

    public override void startCasting(Vector3 referencePoint)
    {
        base.startCasting(referencePoint);
        consumeMana(manaConsumption);
        manaConsumption = 0;
    }

    public override bool canCastSpell(int currMana)
    {
        return currMana >= manaConsumption;
    }
}
