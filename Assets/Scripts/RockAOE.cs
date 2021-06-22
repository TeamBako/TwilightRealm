using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockAOE : MonoBehaviour
{
    // Start is called before the first frame update
    protected int damageOverTime;

    protected float timer;

    protected bool ticked = false;
    public void setup(int dot, float aoe, float dur)
    {
        damageOverTime = dot;
        transform.localScale = new Vector3(aoe, aoe, aoe);
        Destroy(gameObject, dur);
    }

    protected void Update()
    {
        timer += Time.deltaTime;
        if(timer >= 0.25f)
        {
            ticked = false;
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if(!ticked && other.tag == "Monster")
        {
            Debug.Log("ticked:" + damageOverTime);
            ticked = true;
            timer = 0;
            other.GetComponent<AIController>().takeDamage(damageOverTime);
        }
    }

}
