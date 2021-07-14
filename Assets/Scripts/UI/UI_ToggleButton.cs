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
    public Text textButton;
    public bool selfToggle;

    public GameObject toggleArea;

    public bool noImage;

    public void Start()
    {
        if (!noImage)
        {
            image = GetComponent<Image>();
            image.sprite = (isToggled) ? onSprite : offSprite;

        }
        if (textButton != null)
        {
            textButton.text = (isToggled) ? "On" : "Off";
        }
    }

    public void SetToggleState(bool status)
    {
        isToggled = status;

        if (!noImage)
        {
            image.sprite = (isToggled) ? onSprite : offSprite;
        }

        if (textButton != null)
        {
            textButton.text = (isToggled) ? "On" : "Off";
        }

        if (toggleArea != null)
        {
            toggleArea.SetActive(isToggled);
        }
    }

    public void ToggleButton()
    {
        if (!selfToggle)
        {
            if (!isToggled)
            {
                if (toggleGroup != null)
                {
                    toggleGroup.ToggleGroup(this);
                }

            }
        }
        else
        {
            SetToggleState(!isToggled);
        }
    }

}
