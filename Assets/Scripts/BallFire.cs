using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallFire : MonoBehaviour
{
    [SerializeField]
    protected float speedMultiplier;

    protected float areaOfEffect;

    protected int damage;
    // Start is called before the first frame update

    [SerializeField]
    protected GameObject explosion;

    protected bool isGrowing = true;
    public void activate(int dmg, float aoe)
    {
        damage = dmg;
        areaOfEffect = aoe;
        StartCoroutine(grow());
    }

    protected IEnumerator grow()
    {
        isGrowing = true;
        while(Vector3.Distance(transform.localScale, Vector3.one) > 0.05)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime);
            yield return new WaitForSeconds(0.1f);
        }
    }
    // Update is called once per frame
    public void fire(Vector3 pos)
    {
        isGrowing = false;
        StartCoroutine(moveTowardsTarget(pos));
    }

    protected IEnumerator moveTowardsTarget(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 1)//Changes here
        {
            transform.position += Vector3.Normalize(target - transform.position) * speedMultiplier;
            yield return new WaitForEndOfFrame();
        }
        explode();
    }

    protected void OnTriggerEnter(Collider other)
    {
        //StopAllCoroutines();
        Debug.Log(other.gameObject.name);
        if (!isGrowing && (other.gameObject.tag == "Player" || other.gameObject.tag == "Terrain"))
        {
            explode();
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        //StopAllCoroutines();
        if (!isGrowing && (other.gameObject.tag == "Player" || other.gameObject.tag == "Terrain"))
        {
            explode();
        }
    }

    protected bool exploded = false;

    protected void explode()
    {
        if (exploded)
            return;
        exploded = true;
        StopAllCoroutines();
        GameObject ex = Instantiate(explosion, transform.position, Quaternion.identity);
        ex.transform.localScale = new Vector3(areaOfEffect, areaOfEffect, areaOfEffect);
        Collider[] cols = Physics.OverlapSphere(transform.position, areaOfEffect * 5);

        foreach (Collider c in cols)
        {
            if (c.tag == "Player")
            {
                c.GetComponent<EntityStateControl>().takeDamage(damage);
            }
        }
        Destroy(ex.gameObject, 2f);
        Destroy(gameObject, 10f);
        gameObject.SetActive(false);
    }
    public void OnDestroy()
    {
        StopAllCoroutines();
    }
}
