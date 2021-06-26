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

    public virtual void UI_Awake() 
    {
        canvasGroup = GetComponent<CanvasGroup>();

        gameObject.SetActive(true);
    }
    public virtual void UI_Start() { }
    public virtual void UI_Initialize() { }
    public virtual void UI_Update() { }

    public void TogglePanel(bool state)
    {        
        isOn = state;
        switch (transition)
        {
            case Transition.NONE:
                {
                    break;
                }
            case Transition.INSTANT:
                {
                    canvasGroup.alpha = (isOn) ? 1 : 0;
                    break;
                }
        }

        canvasGroup.interactable = isOn;
        canvasGroup.blocksRaycasts = isOn;
    }
}
