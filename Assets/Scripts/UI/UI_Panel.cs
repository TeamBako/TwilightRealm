using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CanvasGroup))]
public abstract class UI_Panel : MonoBehaviour
{
    public Transition transition;

    private CanvasGroup canvasGroup;
    private bool isOn;

    public virtual void UI_Awake() { }
    public virtual void UI_Start() { }
    public virtual void UI_Update() { }

    public void TogglePanel(bool state)
    {
        switch (transition)
        {
            case Transition.NONE:
                {
                    break;
                }
            case Transition.INSTANT:
                {
                    canvasGroup.alpha = (state) ? 1 : 0;
                    break;
                }
        }
        isOn = state;
        canvasGroup.interactable = state;
    }


    private void Awake()
    {
        UI_Awake();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Start is called before the first frame update
    void Start()
    {
        TogglePanel(false);
        UI_Start();
    }
}
