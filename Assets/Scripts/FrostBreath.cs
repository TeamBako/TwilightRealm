using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrostBreath : SpellHandler
{

    [SerializeField]
    private int baseDPS, baseManaConsumptionRate, baseAngle;

    [SerializeField]
    private float baseRange, baseSlowEffect, baseSlowDuration;

    [SerializeField]
    private int dpsPerLevel, anglePerLevel;

    [SerializeField]
    private float rangePerLevel, slowEffectPerLevel, slowDurationPerLevel, manaConsumptionPerLevel;

    protected float tickTimer = 0;

    protected float tickRate = 0.5f;

    [SerializeField]
    protected GameObject slowPrefab;

    protected int manaConsumption
    {
        get
        {
            return Mathf.CeilToInt(baseManaConsumptionRate * Mathf.Pow(manaConsumptionPerLevel,
                refData.frostBreathData.manaConsumptionRate));
        }
    }

    protected float slowEffect
    {
        get
        {
            return baseSlowEffect * Mathf.Pow(slowEffectPerLevel, refData.frostBreathData.slowEffect);
        }
    }

    protected float slowDuration
    {
        get
        {
            return baseSlowDuration + slowDurationPerLevel * refData.frostBreathData.slowDuration;
        }
    }
    protected int damagePerSecond
    {
        get
        {
            return baseDPS + dpsPerLevel * refData.frostBreathData.damagePerSecond;
        }
    }

    protected float range
    {
        get
        {
            return baseRange + rangePerLevel * refData.frostBreathData.areaOfCone;
        }
    }

    protected int width
    {
        get
        {
            int wid = baseAngle + anglePerLevel * refData.frostBreathData.areaOfCone;
            return wid < 80 ? wid : 80;
        }
    }

    public override void setup(Transform _caster, PlayerData data, Action<int> cMana)
    {
        base.setup(_caster, data, cMana);
        ParticleSystem[] ps = GetComponentsInChildren<ParticleSystem>();
        foreach (ParticleSystem s in ps)
        {
            ParticleSystem.MainModule mm = s.main;
            mm.startSpeed = mm.startSpeed.constant * range/baseRange;
            ParticleSystem.ShapeModule sm = s.shape;
            sm.angle = width;
            ParticleSystem.MinMaxCurve em = s.emission.rateOverTime;
            em.constantMax = em.constantMax * Mathf.Pow(range / baseRange, 1);
            em.constantMin = em.constantMin * Mathf.Pow(range / baseRange, 1);
            ParticleSystem.EmissionModule e = s.emission;
            e.rateOverTime = em;
        }
    }

    public override float percentageCompletion()
    {
        return 1;
    }
    public override bool finishedCasting()
    {
        return true;
    }

    protected override void Update()
    {
        base.Update();
        tickTimer += Time.deltaTime;
        if(tickTimer >= tickRate && isCasting)
        {
            tick();
            tickTimer = 0;
        }
    }

    protected void tick()
    {
        consumeMana(manaConsumption);
        Vector3 front = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        List<AIController> damaged = new List<AIController>();
        for(int i = -width; i <= width; i += 5)
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, Quaternion.Euler(0, i, 0) * front, out hit, range))
            {
                if(hit.collider.tag == "Monster")
                {
                    AIController con = hit.collider.GetComponent<AIController>();
                    if (!damaged.Contains(con))
                    {
                        damaged.Add(con);
                        con.takeDamage(Mathf.CeilToInt(damagePerSecond * tickRate));
                        Slow eff = con.gameObject.GetComponentInChildren<Slow>();
                        if(!eff)
                        {
                            eff = Instantiate(slowPrefab, con.transform.position, con.transform.rotation).GetComponent<Slow>();
                            eff.transform.parent = con.transform;
                        }
                        eff.activate(slowEffect, slowDuration);
                    }
                }
            }
        }
        //Collider[] hits = Physics.OverlapSphere(transform.position, range); //get all colliders within sphere
        //foreach(Collider col in hits)
        //{
        //    if (col.tag == "Monster")
        //    {
        //        Vector3 front = new Vector3(transform.forward.x, 0, transform.forward.z).normalized;
        //        Vector3 dir = col.transform.position - transform.position;
        //        dir = new Vector3(dir.x, 0, dir.z).normalized;
        //        float rad = Mathf.Acos(Vector3.Dot(front, dir));
        //        float deg = rad * (180 / Mathf.PI);
        //        Debug.Log(deg);
        //        if (deg <= width)
        //        {
        //            col.GetComponent<AIController>().takeDamage((int)(damagePerSecond * tickRate));
        //        }
        //    }
        //}
    }

    public override bool canCastSpell(int currMana)
    {
        return currMana >= manaConsumption;
    }

    public override void startCasting(Vector3 referencePoint)
    {
        base.startCasting(referencePoint);
        transform.parent = caster;
    }
    // Start is called before the first frame update
    public override void whileCasting(Vector3 referencePoint)
    {
        base.whileCasting(referencePoint);
        //dps dps
        Vector3 pos = referencePoint;
        pos.y = transform.position.y;
        transform.LookAt(pos);
    }


    public override void onFullCast(Vector3 referencePoint)
    {
        isCasting = false;
        base.onFullCast(referencePoint);
        transform.parent = null;
        ParticleSystem[] ps = GetComponentsInChildren<ParticleSystem>();
        foreach(ParticleSystem s in ps)
        {
            ParticleSystem.EmissionModule em = s.emission;
            em.enabled = false;
        }
        Destroy(gameObject, 0.5f);
    }

    
    
}
