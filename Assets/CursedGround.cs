using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedGround : MonoBehaviour
{
    protected float duration = Mathf.Infinity, damageTimer, timer;

    protected int damage;

    protected float tickTimer = 0.5f;
    public void setup(int dmg, float dur)
    {
        damage = dmg;
        duration = dur;
        damageTimer = 0.5f;
        timer = 0;
        
    }

    protected void Update()
    {
        Debug.Log(timer);
        if (timer >= duration)
        {
            gameObject.SetActive(false);
            Destroy(gameObject, 5f);
            return;
        }

        if (damageTimer >= tickTimer)
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, transform.localScale.x * 1.25f);
            foreach(Collider col in cols)
            {
                if(col.tag == "Player")
                {
                    col.GetComponent<EntityStateControl>().takeDamage((int)(damage * tickTimer));
                }
            }
            damageTimer = 0;
        } 
        else
        {
            damageTimer += Time.deltaTime;
            timer += Time.deltaTime;
        }
    }
}
