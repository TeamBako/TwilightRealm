using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedProjectile : MonoBehaviour
{
    [SerializeField]
    protected float speed, areaOfEffect;
    [SerializeField]
    protected GameObject cursedGroundPrefab;
    protected float duration;
    protected int damage;
    public void setup(Vector3 pt, int dmg, float dur)
    {
        damage = dmg;
        duration = dur;
        StartCoroutine(moveTowardsTarget(pt));
    }
    // Start is called before the first frame update
    protected IEnumerator moveTowardsTarget(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 1)//Changes here
        {
            transform.position += Vector3.Normalize(target - transform.position) * speed;
            yield return new WaitForEndOfFrame();
        }
        explode();
    }

    protected void OnTriggerEnter(Collider other)
    {
        //StopAllCoroutines();
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Terrain")
        {
            explode();
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        //StopAllCoroutines();
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "Terrain")
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
        Vector3 spawnPt = transform.position;
        spawnPt.y = 0.25f;
        CursedGround ex = Instantiate(cursedGroundPrefab, spawnPt, cursedGroundPrefab.transform.rotation).GetComponent<CursedGround>();
        ex.setup(damage, duration);
        ex.transform.localScale = new Vector3(areaOfEffect, areaOfEffect, areaOfEffect);
        Destroy(gameObject, 10f);
        gameObject.SetActive(false);
    }
}
