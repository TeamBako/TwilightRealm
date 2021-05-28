using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : SpellHandler
{
    [SerializeField]
    private float speedMultiplier;

    [SerializeField]
    private GameObject explosion; //replace with proper explosion calculations
    public override void onFullCast(Vector3 target)
    {
        StartCoroutine(moveTowardsTarget(target));
    }

    protected IEnumerator moveTowardsTarget(Vector3 target)
    {
        
        while (Vector3.Distance(transform.position, target) > 1)
        {
            transform.position += Vector3.Normalize(target - transform.position);
            yield return new WaitForEndOfFrame();
        }
        Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 2f);
        Destroy(gameObject, 0.01f);
    }

    protected void OnTriggerEnter(Collider other)
    {
        //StopAllCoroutines();
    }
}
