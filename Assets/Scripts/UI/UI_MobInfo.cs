using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MobInfo : MonoBehaviour
{
    public AIController mobInfo;
    public UI_Bar hp_Bar;

    // Start is called before the first frame update
    void Start()
    {
        hp_Bar.UI_Initialize();
    }

    // Update is called once per frame
    void Update()
    {
        LookAtCamera();

        hp_Bar.SetUIPercentage(mobInfo.getCurrentHP()/ (float) mobInfo.getMaxHP());
        hp_Bar.UI_Update();
    }

    public void LookAtCamera()
    {
        Vector3 camPos = Camera.main.transform.position;
        camPos.x = this.transform.position.x;
        this.transform.LookAt(camPos);
    }
}
