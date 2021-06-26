using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    

    [SerializeField]
    private bool gamePaused = false;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        { 
            UIManager.Instance.ToggleUpgradePanel();
        }
    }

    public void TogglePauseGame()
    {
        Time.timeScale = gamePaused ? 1 : 0;
        gamePaused = !gamePaused;
    }
}
