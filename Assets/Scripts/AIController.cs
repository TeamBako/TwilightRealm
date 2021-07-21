using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : EntityStateControl
{

    [SerializeField]
    protected float baseSpeed;

    protected EntityStateControl target;

    [SerializeField]
    protected float range;

    protected bool attacking = false;
    protected void Start()
    {
        target = PlayerControl.Instance;
        enterMState(EntityMovementState.MOVING);
    }

    #region Balance
    [SerializeField]
    protected int baseDamage, damagePerWave;
    [SerializeField]
    protected int baseHP, hpPerWave;
    #endregion

    #region CombatValues
    protected int currentHP = 100, maxHP, damage;
    #endregion

    #region SetByProgression

    public override float getMovementSpeed()
    {
        return baseSpeed;
    }

    public override int getMaxHP()
    {
        return maxHP;
    }

    public override void setMaxHP(int value)
    {
        maxHP = value;
    }

    public override void setCurrentHP(int val)
    {
        currentHP = val;
    }

    public override int getCurrentHP()
    {
        return currentHP;
    }

    protected void setDamage(int dmg)
    {
        damage = dmg;
    }

    public virtual void setup(int waveNo)
    {
        setCurrentHP(baseHP + hpPerWave * waveNo);
        setMaxHP(baseHP + hpPerWave * waveNo);
        setDamage(baseDamage + damagePerWave * waveNo);
    }
    #endregion
    protected bool avoidance = false;
    protected float avoidanceTimer = 0f;
    protected override void handleMovement()
    {
        base.handleMovement();
        
        if (attacking == false)
        {
            if (avoidance && avoidanceTimer < 0.5f)
            {
                rb.velocity = transform.forward * getSpeed();
                avoidanceTimer += Time.deltaTime;
                return;
            } else
            {
                avoidance = false;
                avoidanceTimer = 0f;
            }

            Vector3 tar = (target.transform.position - transform.position).normalized;
            Vector3 dir = tar;
            int layermask = 1 << 8;
            RaycastHit hit;
            if (Physics.Raycast(transform.position + Vector3.up, transform.forward, out hit, 1.5f, layermask) ||
                Physics.Raycast(transform.position + Vector3.up, Quaternion.Euler(0, 45, 0) * transform.forward, out hit, 1.5f, layermask)
                || Physics.Raycast(transform.position + Vector3.up, Quaternion.Euler(0, -45, 0) * transform.forward, out hit, 1.5f, layermask))
            {
                dir = Vector3.Distance(transform.position + Quaternion.Euler(0, 90, 0) * transform.forward, hit.transform.position)
                    < Vector3.Distance(transform.position + Quaternion.Euler(0, -90, 0) * transform.forward, hit.transform.position) 
                    ? Quaternion.Euler(0, -90, 0) * transform.forward :
                    Quaternion.Euler(0, 90, 0) * transform.forward;
                avoidance = true;
                transform.rotation = Quaternion.LookRotation(dir);
                return;
            }
            //else if (Physics.Raycast(transform.position + transform.forward, Quaternion.Euler(0, -45, 0) * transform.forward, 1, layermask))
            //{
            //    dir = Quaternion.Euler(0, 90, 0) * transform.forward;
            //    Debug.Log("hit2");
            //}
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 5 * Time.deltaTime);
            
            if (Vector3.Distance(target.transform.position, transform.position) <= range 
                && Mathf.Abs(Vector3.SignedAngle(tar, transform.forward, Vector3.up)) <= 45f)
            {
                enterCState(EntityCombatState.COMBAT);
            }
            else
            {
                rb.velocity = dir * getSpeed();
            }
        }
    }

    protected override void handleStationary()
    {
        base.handleStationary();
        rb.velocity = Vector3.zero;
    }

    protected override void enterCombat()
    {
        base.enterCombat();
        enterMState(EntityMovementState.STATIONARY);
    }

    protected override void enterNotAttacking()
    {
        base.enterNotAttacking();
        enterMState(EntityMovementState.MOVING);
    }

    protected override void enterMovement()
    {
        base.enterMovement();
        anim.SetBool("Move", true);
    }

    protected override void enterStationary()
    {
        base.enterStationary();
        anim.SetBool("Move", false);
        rb.velocity = Vector3.zero;
    }

    protected override void handleCombat()
    {
        base.handleCombat();
        if (attacking)
        {
            return;
        }
        Vector3 tar = (target.transform.position - transform.position).normalized;
        if (Vector3.Distance(target.transform.position, transform.position) > range
            || Mathf.Abs(Vector3.SignedAngle(tar, transform.forward, Vector3.up)) > 45f)
        {
            enterCState(EntityCombatState.NOTATTACKING);
        }
        else 
        {
            Debug.Log("start");
            attacking = true;
            anim.SetBool("Attack", true);        
        }
    }

    protected override void enterDeath()
    {
        base.enterDeath();
        enterMState(EntityMovementState.STATIONARY);
        anim.SetBool("Death", true);
        StartCoroutine(disableAI());
    }

    protected IEnumerator disableAI()
    {
        yield return new WaitForSeconds(5);
        gameObject.SetActive(false);
    }
    public virtual void attackEvent()
    {
        target.takeDamage(damage);
    }

    public virtual void attackEnd()
    {
        attacking = false;
        anim.SetBool("Attack", false);
        Debug.Log("end");
    }
}
