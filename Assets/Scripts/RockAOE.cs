using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAOE : MonoBehaviour
{
    // Start is called before the first frame update
    protected int damageOverTime;

    protected float areaOfEffect;

    protected float timer;

    public void setup(int dot, float aoe, float dur)
    {
        damageOverTime = dot;
        transform.localScale = new Vector3(aoe, aoe, aoe);
        areaOfEffect = aoe;
        Destroy(gameObject, dur);
    }

    protected void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.5f)
        {
            timer = 0;
            Collider[] cols = Physics.OverlapSphere(transform.position, areaOfEffect * 5);
            
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
