using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    protected int damage;
    public void setDamage(int val)
    {
        damage = val;
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Monster")
        {
            other.GetComponent<AIController>().takeDamage(damage);
        }
    }
}
