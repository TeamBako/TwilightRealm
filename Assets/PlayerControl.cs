using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : EntityStateControl
{
    // Start is called before the first frame update
    protected override void handleStationary()
    {
        base.handleStationary();
        handleRotation();
        speed = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")); //temp
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
        handleRotation();
        base.handleMovement();
    }

    protected void handleRotation()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100))
        {
            hit.point = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.LookAt(hit.point);
        }
    }

    protected override void handleIdle()
    {

    }

    protected override void handleCombat()
    {

    }

    protected override void handleDeath()
    {

    }

    

}
