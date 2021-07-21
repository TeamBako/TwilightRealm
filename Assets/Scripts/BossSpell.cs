using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BossSpell : MonoBehaviour
{
    protected int damage, level;
    // Start is called before the first frame update
    public virtual void setup(int lev, int dmg)
    {
        damage = dmg;
        level = lev;
    }

    public abstract void cast(Vector3 refPoint);
}
