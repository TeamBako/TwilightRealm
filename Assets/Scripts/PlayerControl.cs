using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : EntityStateControl
{
    public static PlayerControl Instance;

    [SerializeField]
    private PlayerData pData;

    #region Balance
    [SerializeField]
    private int baseHP, hpPerLevel, baseMP, mpPerLevel, baseHPRegen, baseMPRegen, hPRegenPerLevel, mPRegenPerLevel;
    [SerializeField]
    private float baseSpeed, speedPerLevel;
    #endregion

    #region UnsavableCombat
    [SerializeField]
    private int currentMP, currentHP;
    private float regenTimer = 0f;
    #endregion

    #region CombatRelated

    [SerializeField]
    private SpellHandler fireBall, frostBreath, rockDOT;

    private SpellHandler selectedSpell;

    private SpellHandler castedSpell;

    [SerializeField]
    private Transform castPoint;

    private bool casting = false;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        Instance = this;
    }

    public void activate(PlayerData data)
    {
        pData = data;
        currentHP = getMaxHP();
        currentMP = getMaxMP();
    }

    public void reset()
    {
        currentHP = getMaxHP();
        currentMP = getMaxMP();
    }

    public PlayerData PlayerData
    {
        get { return pData; }
        set { pData = value; }
    }

    protected int getHPRegen()
    {
        return baseHPRegen + hPRegenPerLevel * pData.hPRegen;
    }

    protected int getMPRegen()
    {
        return baseMPRegen + mPRegenPerLevel * pData.mPRegen;
    }

    public override float getMovementSpeed()
    {
        return baseSpeed + speedPerLevel * pData.movementSpeed;
    }

    public int getMaxMP()
    {
        return baseMP + pData.maxMP * mpPerLevel;
    }

    public int getCurrentMP()
    {
        return currentMP;
    }

    public override int getMaxHP()
    {
        return baseHP + pData.maxHP * hpPerLevel;
    }

    public override void setMaxHP(int value)
    {
        pData.maxHP = value;
    }

    public override int getCurrentHP()
    {
        return currentHP;
    }

    public override void setCurrentHP(int val)
    {
        currentHP = val;
    }
    public PlayerData deactivate()
    {
        //make change to pData here before returning
        return pData;
    }

    #region StateHandling
    protected override void handleStationary()
    {
        base.handleStationary();
        Vector3 dir = (getMousePositionInWorldSpace(transform.position.y) - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 100 * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            enterMState(EntityMovementState.MOVING);
        }//temp
    }

    protected override void enterDeath()
    {
        base.enterDeath();
        enterMState(EntityMovementState.STATIONARY);
        StopAllCoroutines();
        if (castedSpell)
        {
            castedSpell.gameObject.SetActive(false);
            Destroy(castedSpell.gameObject, 10f);
        }
        anim.SetBool("Death", true);
        UIManager.Instance.DisplayPlayerDead();
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

    protected override void handleMovement()
    {
        float horizontalDir = 0;
        float verticalDir = 0;
        if (Input.GetKey(KeyCode.W))
        {
            verticalDir += 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            verticalDir -= 1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            horizontalDir -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            horizontalDir += 1;
        }
        Vector3 speed = new Vector3(horizontalDir, 0, verticalDir) * getSpeed();
        Vector3 dir = (getMousePositionInWorldSpace(transform.position.y) - transform.position).normalized;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 100 * Time.deltaTime);
        if(speed == Vector3.zero)
        {
            enterMState(EntityMovementState.STATIONARY);
        } else
        {
            rb.velocity = speed;
        }
        base.handleMovement();
    }

    protected override void handleNotAttacking()
    {
        currentCState = EntityCombatState.COMBAT;
    }

    protected override void handleCombat()
    {
        if (regenTimer >= 1)
        {
            currentHP = getHPRegen() + currentHP > getMaxHP() ? getMaxHP() : getHPRegen() + currentHP;
            currentMP = getMPRegen() + currentMP > getMaxMP() ? getMaxMP() : getMPRegen() + currentMP;
            Debug.Log(getMPRegen());
            regenTimer = 0;
        }
        else
        {
            regenTimer += Time.deltaTime;
        }

        if (casting == false && !GameManager.Instance.gamePaused)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                selectedSpell = fireBall;
                UIManager.Instance.SetSkillHighlight(SpellType.FIRE);
            }
            else if (Input.GetKeyDown(KeyCode.R))
            {
                selectedSpell = frostBreath;
                UIManager.Instance.SetSkillHighlight(SpellType.ICE);
            }
            else if (Input.GetKeyDown(KeyCode.T))
            {
                selectedSpell = rockDOT;
                UIManager.Instance.SetSkillHighlight(SpellType.EARTH);
            }
            else if (Input.GetMouseButtonDown(0) && selectedSpell != null)
            {
                SpellHandler spell = null;
                
                spell = Instantiate(selectedSpell, castPoint.transform.position,
                    selectedSpell.transform.rotation).GetComponent<SpellHandler>();
                System.Action<int> consumeMana = x => currentMP -= x;
                spell.setup(castPoint, pData, consumeMana);
                if (spell.canCastSpell(currentMP))
                {
                    //Set values accordingly
                    casting = true;
                    StartCoroutine(castSpell(spell));
                } else
                {
                    spell.gameObject.SetActive(false);
                    Destroy(spell.gameObject, 10f);
                }
            }
        }
    }

    #endregion

    private IEnumerator castSpell(SpellHandler handler) //need to redefine castTime
    {
        handler.startCasting(getMousePositionInWorldSpace(castPoint.transform.position.y));
        castedSpell = handler;
        while (Input.GetMouseButton(0))
        {
            if (handler.canCastSpell(currentMP))
            {
                handler.whileCasting(getMousePositionInWorldSpace(castPoint.transform.position.y));
                yield return new WaitForSeconds(0.1f);

            } else
            {
                break;
            }
            
        }

        if (handler.finishedCasting())
        {
            handler.onFullCast(getMousePositionInWorldSpace(castPoint.transform.position.y));
        } else
        {
            handler.onDisruptedCast(getMousePositionInWorldSpace(castPoint.transform.position.y));
        }
        casting = false;
        castedSpell = null;
    }

    public SpellHandler spellCasted
    {
        get
        {
            return castedSpell;
        }
    }
    private Vector3 getMousePositionInWorldSpace(float yPos)
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            hit.point = new Vector3(hit.point.x, yPos, hit.point.z);
            return hit.point;
        } else
        {
            return new Vector3(0, yPos, 0);
        }
    }
}
