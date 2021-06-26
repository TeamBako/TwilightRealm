using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgradeField : MonoBehaviour
{
    public Text levelDesc;
    public Button button;
    public Text buttonText;

    public void SetButtonInteractable(bool status)
    {
        button.interactable = status;
        buttonText.gameObject.SetActive(status);
    }

    public void UpdateFieldInfo(int level)
    {
        levelDesc.text = "(Level " + (level + 1) + ")";
    }
}

