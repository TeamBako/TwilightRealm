using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparkProjectile : MonoBehaviour
{
    protected float tickTimer = 0.5f;

    protected Vector3 refPoint, dir;

    protected int damage;

    [SerializeField]
    protected float speed, duration, timer;

    [SerializeField]
    protected float damageTimer = 0f, dirTimer = 0f;

    protected bool destroyed = false;

    protected Rigidbody rb;

    protected void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void setup(Vector3 pt, int dmg, float spd, float dur)
    {
        refPoint = pt;
        damage = dmg;
        speed = spd;
        dir = getRandomNormVector();
        duration = dur;
        timer = 0;
    }

    public Vector3 getRandomNormVector()
    {
        float x = Random.Range(-100, 100);
        float z = Random.Range(-100, 100);
        return new Vector3(x, 0, z).normalized;
    }

    protected void Update()
    {
        if (destroyed)
        {
            return;
        }

        if(timer >= duration)
        {
            destroyed = true;
            gameObject.SetActive(false);
            Destroy(gameObject, 10f);
            return;
        }

        if(dirTimer >= 2f)
        {
            dir = getRandomNormVector();
            dirTimer = 0f;
        }
        dirTimer += Time.deltaTime;
        rb.velocity = dir * speed;
        damageTimer += Time.deltaTime;
        timer += Time.deltaTime;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Terrain")
        {
            float x = transform.position.x - other.transform.position.x;
            float z = transform.position.z - other.transform.position.z;
            dir = new Vector3(x, 0, z).normalized;
        } 

        if(other.tag == "Player" && damageTimer > tickTimer)
        {
            other.GetComponent<EntityStateControl>().takeDamage((int)(damage * tickTimer));
            damageTimer = 0f;
        }
    }

    protected void OnTriggerStay(Collider other)
    {
        if (other.tag == "Terrain")
        {
            float x = transform.position.x - other.transform.position.x;
            float z = transform.position.z - other.transform.position.z;
            dir = new Vector3(x, 0, z).normalized;
        }

        if (other.tag == "Player" && damageTimer > tickTimer)
        {
            other.GetComponent<EntityStateControl>().takeDamage((int)(damage * tickTimer));
            damageTimer = 0f;
            Debug.Log("damaged");
        }
    }


}
