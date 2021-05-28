using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : EntityStateControl
{
    private PlayerData data;

    private Animator anim;

    #region Temp
    [SerializeField]
    private int spellNumber;
    #endregion

    #region CombatRelated

    [SerializeField]
    private List<SpellHandler> spells = new List<SpellHandler>();

    [SerializeField]
    private Transform castPoint;

    private bool casting = false;

    #endregion

    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }

    #region StateHandling
    protected override void handleStationary()
    {
        base.handleStationary();
        transform.LookAt(getMousePositionInWorldSpace());
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.S)
            || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
        {
            enterMState(EntityMovementState.MOVING);
        }//temp
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
        speed = new Vector3(horizontalDir, 0, verticalDir) * speedMultiplier;
        transform.LookAt(getMousePositionInWorldSpace());
        if(speed == Vector3.zero)
        {
            enterMState(EntityMovementState.STATIONARY);
        } else
        {
            rb.velocity = speed;
        }
        base.handleMovement();
    }

    protected override void handleIdle()
    {
        currentCState = EntityCombatState.COMBAT;
    }

    protected override void handleCombat()
    {
        if (casting == false && Input.GetMouseButtonDown(0))
        {
            casting = true;//May want to properly classify combat in the future
            SpellHandler spell = Instantiate(spells[spellNumber], castPoint.transform).GetComponent<SpellHandler>();
            //Set values accordingly
            StartCoroutine(castSpell(spell));
        }
    }

    #endregion

    private IEnumerator castSpell(SpellHandler handler, float castTime = 0.01f) //need to redefine castTime
    {
        float timer = 0;
        while (Input.GetMouseButton(0))
        {
            handler.whileCasting(getMousePositionInWorldSpace());
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        handler.transform.parent = null;
        if (timer >= castTime)
        {
            handler.onFullCast(getMousePositionInWorldSpace());
        } else
        {
            handler.onDisruptedCast(getMousePositionInWorldSpace());
        }
        casting = false;
    }

    private Vector3 getMousePositionInWorldSpace()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            hit.point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            return hit.point;
        } else
        {
            return Vector3.zero;
        }
    }
}
