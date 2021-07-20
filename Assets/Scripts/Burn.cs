using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
    protected int burnDamage;
    protected float duration;
    protected EntityStateControl target;

    protected float tickTimer = 0f;
    public virtual void activate(int burnDam, float dur)
    {
        target = GetComponentInParent<EntityStateControl>();
        burnDamage = burnDam;
        duration = dur;
    }

    // Update is called once per frame
    void Update()
    {
        if(duration <= 0)
        {
            Destroy(gameObject, 0.1f);
            return;
        }

        if(tickTimer >= 1)
        {
            tickTimer = 0;
            target.takeDamage(burnDamage);
        } else
        {
            tickTimer += Time.deltaTime;
        }

        duration -= Time.deltaTime;
    }
}
