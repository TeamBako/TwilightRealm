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
        IDLE,
        COMBAT,
        DEATH
    }

    [SerializeField]
    protected float speedMultiplier;

    protected Vector3 speed;

    protected EntityMovementState currentMState;

    protected EntityCombatState currentCState;

    protected Rigidbody rb;

    protected virtual void Awake()
    {
        currentMState = EntityMovementState.STATIONARY;
        currentCState = EntityCombatState.IDLE;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    protected void Update()
    {
        switch(currentMState)
        {
            case EntityMovementState.STATIONARY:
                handleStationary();
                break;
            case EntityMovementState.MOVING:
                handleMovement();
                break;
        }
        
        switch(currentCState)
        {
            case EntityCombatState.COMBAT:
                handleCombat();
                break;
            case EntityCombatState.IDLE:
                handleIdle();
                break;
            case EntityCombatState.DEATH:
                handleDeath();
                break;
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
            case EntityCombatState.IDLE:
                enterIdle();
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

    protected virtual void handleIdle() { }

    protected virtual void handleCombat() { }

    protected virtual void handleDeath() { }

    protected virtual void enterStationary() { }

    protected virtual void enterMovement() { }

    protected virtual void enterIdle() { }

    protected virtual void enterCombat() { }

    protected virtual void enterDeath() { }



}
