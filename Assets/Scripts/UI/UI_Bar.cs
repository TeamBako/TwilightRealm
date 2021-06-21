using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Bar : MonoBehaviour
{
    public RectTransform fill;
    public RectTransform mask;
    public float maxSize;

    private float curSize;

    private RectTransform ui_RectTransform;
    private Vector2 maxBarSize;
    private Vector2 curBarSize;

    // Start is called before the first frame update
    void Start()
    {
        this.ui_RectTransform = GetComponent<RectTransform>();
        this.maxBarSize = ui_RectTransform.rect.size;
        this.curBarSize = maxBarSize;
    }

    public void UI_Initialize()
    {
        curSize = maxSize;
        mask.sizeDelta = curBarSize;
    }

    public void SetUIMaxSize(float size)
    {
        maxBarSize.x = size;
        this.ui_RectTransform.sizeDelta = maxBarSize;
        fill.sizeDelta = maxBarSize;
    }

    public void SetUIPercentage(float percentage)
    {
        curSize = percentage * maxSize;
    }

    public void UI_Update()
    {
        curBarSize.x = curSize;
        mask.sizeDelta = curBarSize;
    }

}
