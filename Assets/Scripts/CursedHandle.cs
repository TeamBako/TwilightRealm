using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursedHandle : BossSpell
{
    [SerializeField]
    protected float safeRadius, maxRadius;
    // Start is called before the first frame update
    [SerializeField]
    protected int baseProjectile, projectilePerLevel;

    [SerializeField]
    protected GameObject projPrefab;

    [SerializeField]
    protected float baseDuration, durationPerLevel;

    public override void cast(Vector3 refPoint)
    {
        for(int i = 0; i < baseProjectile + projectilePerLevel * level; i++)
        {
            CursedProjectile proj = Instantiate(projPrefab, 
                transform.position, Quaternion.identity).GetComponent<CursedProjectile>();
            Vector3 targetPt = refPoint;
            targetPt.x = targetPt.x + randomGeneration(safeRadius, maxRadius);
            targetPt.y = 0;
            targetPt.z = targetPt.z + randomGeneration(safeRadius, maxRadius);
            proj.setup(targetPt, damage, baseDuration + durationPerLevel * level);
        }
        gameObject.SetActive(false);
        Destroy(gameObject, 10f);
    }

    protected float randomGeneration(float min, float max)
    {
        float ran = Random.Range(safeRadius, maxRadius);
        ran *= Mathf.Sign(Random.Range(-1, 1));
        return ran;
    }
}
