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

    private void Awake()
    {
        currentMState = EntityMovementState.STATIONARY;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
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
                break;
            case EntityCombatState.IDLE:
                break;
            case EntityCombatState.DEATH:
                break;
        }
    }

    protected virtual void handleStationary()
    {
        if (speed != Vector3.zero)
        {
            currentMState = EntityMovementState.MOVING;
        }
    }
    protected virtual void handleMovement()
    {
        rb.velocity = speed;
        if (speed == Vector3.zero)
        {
            currentMState = EntityMovementState.STATIONARY;
        }
    }

    protected abstract void handleIdle();

    protected abstract void handleCombat();

    protected abstract void handleDeath();

    
}
