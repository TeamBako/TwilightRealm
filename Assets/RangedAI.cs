using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAI : AIController
{
    [SerializeField]
    protected BallFire ballFirePrefab;

    [SerializeField]
    protected GameObject firingPoint;

    protected BallFire currentBallFire;

    protected override void handleStationary()
    {
        base.handleStationary();
        Vector3 tar = (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(tar);
    }
    public virtual void attackStart()
    {
        if (currentBallFire)
        {
            Destroy(currentBallFire.gameObject);
        }
        attacking = true;
        currentBallFire = Instantiate(ballFirePrefab, firingPoint.transform.position,
            firingPoint.transform.rotation).GetComponent<BallFire>();
        currentBallFire.transform.parent = firingPoint.transform;
        currentBallFire.activate(damage, 1);
    }
    // Start is called before the first frame update
    public override void attackEvent()
    {
        currentBallFire.fire(target.transform.position);
        currentBallFire.transform.parent = null;
        currentBallFire = null;
    }

    protected override void enterDeath()
    {
        base.enterDeath();
        if(currentBallFire)
        {
            Destroy(currentBallFire.gameObject);
        }
    }
}
