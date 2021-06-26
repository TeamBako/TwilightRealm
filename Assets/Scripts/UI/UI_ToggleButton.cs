using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ToggleButton : MonoBehaviour
{
    public Sprite onSprite;
    public Sprite offSprite;

    public UI_ToggleGroup toggleGroup;
    public bool isToggled;

    private Image image;

    public void Start()
    {
        image = GetComponent<Image>();
        image.sprite = (isToggled) ? onSprite : offSprite;
    }

    public void SetToggleState(bool status)
    {
        isToggled = status;
        image.sprite = (isToggled) ? onSprite : offSprite;
    }

    public void ToggleButton()
    {
        if(!isToggled)
        {
            toggleGroup.ToggleGroup(this);
        }
    }

}
