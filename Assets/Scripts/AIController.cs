using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : EntityStateControl
{
    protected EntityStateControl target;

    [SerializeField]
    protected float range;

    protected bool attacking = false;
    protected void Start()
    {
        target = PlayerControl.Instance;
        enterMState(EntityMovementState.MOVING);
    }

    #region CombatValues
    [SerializeField]
    protected int damage;
    [SerializeField]
    protected int currentHP, maxHP;
    #endregion

    #region SetByProgression


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

    public virtual void setDamage(int value)
    {
        damage = value;
    }
    #endregion

    protected override void handleMovement()
    {
        base.handleMovement();
        Vector3 dir = (target.transform.position - transform.position).normalized;
        Vector3 speed = dir * speedMultiplier;
        transform.LookAt(
            new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
        if (Vector3.Distance(target.transform.position, transform.position) <= range)
        {
            enterCState(EntityCombatState.COMBAT);
        }
        else
        {
            rb.velocity = speed;
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
    }

    protected override void handleCombat()
    {
        base.handleCombat();
        if(Vector3.Distance(target.transform.position, transform.position) > range)
        {
            enterCState(EntityCombatState.NOTATTACKING);
        }
        else if(!attacking)
        {
            attacking = true;
            anim.SetBool("Attack", true);
        }
    }

    public virtual void attackEvent()
    {
        Debug.Log("hit");
        target.takeDamage(damage);
    }

    public virtual void attackEnd()
    {
        attacking = false;
        anim.SetBool("Attack", false);
    }
}
