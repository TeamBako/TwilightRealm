using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAOE : MonoBehaviour
{
    // Start is called before the first frame update
    protected int damageOverTime;

    protected float areaOfEffect;

    protected float timer;

    [SerializeField]
    protected List<ParticleSystem> rocks;

    [SerializeField]
    protected List<ParticleSystem> scalable;

    public void setup(int dot, float aoe, float dur)
    {
        damageOverTime = dot;
        areaOfEffect = aoe;
        foreach (ParticleSystem ps in rocks)
        {
            ParticleSystem.ShapeModule sh = ps.shape;
            sh.radius = sh.radius * aoe;
            ParticleSystem.MinMaxCurve em = ps.emission.rateOverTime;
            em.constant = em.constant * aoe;
            ParticleSystem.EmissionModule e = ps.emission;
            e.rateOverTime = em;
        }

        foreach (ParticleSystem ps in scalable)
        {
            ps.transform.localScale = new Vector3(aoe, aoe, aoe);
        }
        Destroy(gameObject, dur);
    }

    protected void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.5f)
        {
            timer = 0;
            Collider[] cols = Physics.OverlapSphere(transform.position, areaOfEffect * 4.5f);
            
            foreach(Collider c in cols)
            {
                if (c.tag == "Monster")
                {
                    c.GetComponent<AIController>().takeDamage(damageOverTime / 2);
                }
            }
        }
    }

    

}
