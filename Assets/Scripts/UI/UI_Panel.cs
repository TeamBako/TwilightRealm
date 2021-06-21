using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_Panel : MonoBehaviour
{

    public virtual void UI_Awake() { }
    public virtual void UI_Start() { }
    public virtual void UI_Update() { }

    private void Awake()
    {
        UI_Awake();
    }

    // Start is called before the first frame update
    void Start()
    {
        UI_Start();
    }

    // Update is called once per frame
    void Update()
    {
        UI_Update();
    }
}
