using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityStateControl : MonoBehaviour
{
    protected enum EntityMovementState
    {
        STATIONARY,
        MOVING,
    }

    protected enum EntityCombatState
    {
        NOTATTACKING,
        COMBAT,
        DEATH
    }

    [SerializeField]
    protected float speedMultiplier;

    protected EntityMovementState currentMState;

    [SerializeField]
    protected EntityCombatState currentCState;

    protected Animator anim;

    protected Rigidbody rb;

    protected virtual void Awake()
    {
        currentMState = EntityMovementState.STATIONARY;
        currentCState = EntityCombatState.NOTATTACKING;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {
        if (currentCState != EntityCombatState.DEATH)
        {
            switch (currentMState)
            {
                case EntityMovementState.STATIONARY:
                    handleStationary();
                    break;
                case EntityMovementState.MOVING:
                    handleMovement();
                    break;
            }

            switch (currentCState)
            {
                case EntityCombatState.COMBAT:
                    handleCombat();
                    break;
                case EntityCombatState.NOTATTACKING:
                    handleNotAttacking();
                    break;
                case EntityCombatState.DEATH:
                    handleDeath();
                    break;
            }
        }
    }

    protected virtual void enterCState(EntityCombatState cState)
    {
        currentCState = cState;
        switch (currentCState)
        {
            case EntityCombatState.COMBAT:
                enterCombat();
                break;
            case EntityCombatState.NOTATTACKING:
                enterNotAttacking();
                break;
            case EntityCombatState.DEATH:
                enterDeath();
                break;
        }
    }

    protected virtual void enterMState(EntityMovementState mState)
    {
        currentMState = mState;
        switch (currentMState)
        {
            case EntityMovementState.STATIONARY:
                enterStationary();
                break;
            case EntityMovementState.MOVING:
                enterMovement();
                break;
        }
    }

    protected virtual void handleStationary() { }

    protected virtual void handleMovement() { }

    protected virtual void handleNotAttacking() { }

    protected virtual void handleCombat() { }

    protected virtual void handleDeath() { }

    protected virtual void enterStationary() { }

    protected virtual void enterMovement() { }

    protected virtual void enterNotAttacking() { }

    protected virtual void enterCombat() { }

    protected virtual void enterDeath() { }

    public virtual void takeDamage(int damageVal) {
        int newVal = getCurrentHP() - damageVal;
        newVal = newVal < 0 ? 0 : newVal;
        setCurrentHP(newVal);
        if(getCurrentHP() == 0)
        {
            enterCState(EntityCombatState.DEATH);
        }
    }

    public bool isDead()
    {
        return currentCState == EntityCombatState.DEATH;
    }

    public abstract int getCurrentHP();

    public abstract void setCurrentHP(int val);
    public abstract int getMaxHP();//Can be used by UIManager

    public abstract void setMaxHP(int value);//Can be used by GameManagerProgression for AI

}
