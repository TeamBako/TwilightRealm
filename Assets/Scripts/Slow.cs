using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
    // Start is called before the first frame update
    protected EntityStateControl target;

    protected float value, duration;
    public void activate(float val, float dur)
    {
        target = GetComponentInParent<EntityStateControl>();
        this.value = val;
        this.duration = dur;
    }

    protected void Update()
    {
        if(duration <= 0)
        {
            target.setSpeedMultiplier(1);
            gameObject.SetActive(false);
            Destroy(gameObject, 10f);
            return;
        }
        duration -= Time.deltaTime;
        target.setSpeedMultiplier(value);
    }
    // Update is called once per frame
    private void OnDestroy()
    {
        target.setSpeedMultiplier(1);
    }
}
