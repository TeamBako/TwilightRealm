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

    #region Balance
    [SerializeField]
    protected int baseDamage, damagePerWave;
    [SerializeField]
    protected int baseHP, hpPerWave;
    #endregion

    #region CombatValues
    protected int currentHP, maxHP, damage;
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

    protected void setDamage(int dmg)
    {
        damage = dmg;
    }

    public void setup(int waveNo)
    {
        setCurrentHP(baseHP + hpPerWave * waveNo);
        setMaxHP(baseHP + hpPerWave * waveNo);
        setDamage(baseDamage + damagePerWave * waveNo);
    }
    #endregion

    protected override void handleMovement()
    {
        base.handleMovement();
        if (attacking == false)
        {
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
    }
}
