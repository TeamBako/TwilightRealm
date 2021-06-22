using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Skill : MonoBehaviour
{
    public RectTransform fill;
    public float speed = 0.1f;

    private float curSize;
    private float maxSize = 1;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UI_Initialize()
    {
        UI_SetActive(false);
    }

    public void UI_SetActive(bool active)
    {
        this.fill.gameObject.SetActive(active);
        curSize = 0;
        fill.GetComponent<Image>().fillAmount = curSize;
    }

    public bool UI_GetActive()
    {
        return this.fill.gameObject.activeSelf;
    }

    public void SetUIPercentage(float percentage)
    {
    }

    public void UI_Update()
    {
        if(curSize < maxSize)
        {
            curSize += speed;
            fill.GetComponent<Image>().fillAmount = curSize;
        }
    }
}
