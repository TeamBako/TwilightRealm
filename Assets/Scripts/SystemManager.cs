using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{

    public static SystemManager Instance;

    private string playerFileName = "PlayerInfo";
    private string gameFileName = "GameInfo";


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        activatePlayer();
        activateGame();
        loadGameSettings();
        UIManager.Instance.UI_Initialize();
    }

    private void activatePlayer()
    {
        PlayerData pd = new PlayerData();
        object o = SerializeManager.Load(playerFileName);

        if (o != null)
        {
            pd = (PlayerData)o;
        }

        PlayerControl.Instance.activate(pd);
    }

    private void activateGame()
    {
        GameData gd = new GameData();
        object o = SerializeManager.Load(gameFileName);

        if (o != null)
        {
            gd = (GameData)o;
        }

        GameManager.Instance.activate(gd);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ResetPlayerData()
    {
        PlayerControl.Instance.activate(new PlayerData());
        GameManager.Instance.activate(new GameData());
    }

    private void savePlayerProgress()
    {
        SerializeManager.Save(playerFileName, PlayerControl.Instance.deactivate());
        SerializeManager.Save(gameFileName, GameManager.Instance.deactivate());
    }

    private void loadGameSettings()
    {
        if (PlayerPrefs.HasKey("autoWave"))
        {
            GameManager.Instance.autoWaveStart = PlayerPrefs.GetInt("autoWave") != 0;
        }
    }

    private void saveGameSettings()
    {
        PlayerPrefs.SetInt("autoWave", GameManager.Instance.autoWaveStart ? 1 : 0);
    }

    private void OnDestroy()
    {
        savePlayerProgress();
        saveGameSettings();
    }

}
