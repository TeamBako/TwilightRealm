using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheatManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            GameManager.Instance.increaseWave();
        } else if (Input.GetKeyDown(KeyCode.J))
        {
            UIManager.Instance.hackUpgrade();
        }
    }
}
