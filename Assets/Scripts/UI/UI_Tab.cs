using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Tab : MonoBehaviour
{
    public void SetTabActive(bool status)
    {
        this.gameObject.SetActive(status);
    }
}
