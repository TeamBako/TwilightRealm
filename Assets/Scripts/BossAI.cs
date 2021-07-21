using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : AIController
{
    [SerializeField]
    protected int levelRef;

    protected BossSpell currentSpell;

    [SerializeField]
    protected List<BossSpell> spells;
    
    [SerializeField]
    protected Transform firingPoint;

    [SerializeField]
    protected float cooldown;

    public override void setup(int waveNo)
    {
        base.setup(waveNo);
        levelRef = waveNo/15;
    }
    protected override void handleStationary()
    {
        base.handleStationary();
        Vector3 tar = (target.transform.position - transform.position).normalized;
        transform.rotation = Quaternion.LookRotation(tar);
    }

    public virtual void attackStart()
    {
        if (currentSpell)
        {
            Destroy(currentSpell.gameObject);
        }
        attacking = true;
        int index = Random.Range(0, spells.Count - 1);
        currentSpell = Instantiate(spells[index], firingPoint.transform.position,
            firingPoint.transform.rotation).GetComponent<BossSpell>();
        currentSpell.transform.parent = firingPoint.transform;
        currentSpell.setup(levelRef, damage);
    }

    public override void attackEvent()
    {
        if (getCurrentHP() <= 0)
            return;
        currentSpell.cast(target.transform.position);
        currentSpell.transform.parent = null;
        currentSpell = null;
        anim.SetBool("Attack", false);
        StartCoroutine(cooldownPhase());
    }

    protected IEnumerator cooldownPhase()
    {
        float timer = 0f;
        while(timer < cooldown)
        {
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        attacking = false;
    }
    protected override void enterDeath()
    {
        base.enterDeath();
        if (currentSpell)
        {
            Destroy(currentSpell.gameObject);
        }
    }
}
