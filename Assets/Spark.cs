using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spark : BossSpell
{
    [SerializeField]
    protected int baseProjectile, projectilePerLevel;

    [SerializeField]
    protected SparkProjectile projPrefab;

    [SerializeField]
    protected float baseDuration, durationPerLevel, baseSpeed, speedPerLevel;
    public override void cast(Vector3 refPoint)
    {
        for(int i = 0; i < baseProjectile + projectilePerLevel * level; i++)
        {
            SparkProjectile instance = Instantiate(projPrefab, transform.position,
                transform.rotation).GetComponent<SparkProjectile>();
            instance.setup(refPoint, damage, baseSpeed + speedPerLevel * level, baseDuration + durationPerLevel * level);
        }
        gameObject.SetActive(false);
        Destroy(gameObject, 10f);
    }
}
