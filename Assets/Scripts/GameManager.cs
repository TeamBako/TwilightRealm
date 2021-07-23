using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Vector3 spawnLocationBounds;

    private GameData pGameData;
    private HighscoreData pHSData;

    [SerializeField]
    private MobSpawningInformation mobSpawningInfo;

    // Radius away from player
    private float spawningRadius = 20;
    private PlayerControl player;

    public bool gamePaused = false;

    [SerializeField]
    private bool waveStarted = false;
    private int currentWaveMobQuantity = 0;

    public bool autoWaveStart = false;
    public bool isGameOver = false;

    [SerializeField]
    private List<AIController> spawnedmobList = new List<AIController>();

    private void Awake()
    {
        Instance = this;
        Random.InitState((int) System.DateTime.Now.Ticks);                          
    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerControl.Instance;
        UnpauseGame();
    }

    public void HS_Activate(HighscoreData hsData)
    {
        pHSData = hsData;
    }

    public HighscoreData HS_Deactivate()
    {
        return pHSData;
    }

    public void SaveHS(string inputName)
    {
        isGameOver = true;
        pHSData.AddHighscore(new Highscore_Info(inputName, pGameData.waveNo));

        SystemManager.Instance.savePlayerProgress();
        SystemManager.Instance.ResetPlayerData();
    }

    public void activate(GameData data)
    {
        pGameData = data;
        if (!SystemManager.Instance.isMenu && !isGameOver)
        {
            UIManager.Instance.SetWaveStatus(pGameData.waveNo);
        }
    }

    public GameData deactivate()
    {
        return pGameData;
    }
    
    public Vector3 CalculatePosSpawn()
    {
        Vector3 playerPostion = player.GetComponent<Transform>().position;

        Vector3 spawnPosition = new Vector3(0, 0, 0);

        // x coord
        spawnPosition.x = Random.Range((playerPostion.x + spawningRadius), spawnLocationBounds.x); // upper bound;
        float tempXLowerBound = Random.Range((playerPostion.x - spawningRadius), -spawnLocationBounds.x); // lower bound;
        spawnPosition.x = (spawnPosition.x <= spawnLocationBounds.x)
                                ? (tempXLowerBound >= -spawnLocationBounds.x)
                                    ? (Random.Range(0.0f, 1.0f) >= 0.5f)
                                        ? spawnPosition.x
                                        : tempXLowerBound
                                    : spawnLocationBounds.x
                                : tempXLowerBound;
        // z coord
        spawnPosition.z = Random.Range((playerPostion.z + spawningRadius), spawnLocationBounds.z); // upper bound;
        float tempZLowerBound = Random.Range((playerPostion.z - spawningRadius), -spawnLocationBounds.z); // lower bound;
        spawnPosition.z = (spawnPosition.z <= spawnLocationBounds.z)
                                ? (tempZLowerBound >= -spawnLocationBounds.z)
                                    ? (Random.Range(0.0f, 1.0f) >= 0.5f)
                                        ? spawnPosition.z
                                        : tempZLowerBound
                                    : spawnLocationBounds.z
                                : tempZLowerBound;

        return spawnPosition;
    }

    public void StartWave()
    {
        if(spawnedmobList.Count > 0)
        {
            foreach(AIController mob in spawnedmobList)
            {
                Destroy(mob.gameObject, 5f);
            }

            spawnedmobList = new List<AIController>();
        }

        int mobQuantity = mobSpawningInfo.GetNoOfMobToSpawn(pGameData.waveNo);

        if (mobSpawningInfo.CheckBossSpawn(pGameData.waveNo))
        {
            GameObject bossPrefab = mobSpawningInfo.GetBoss();
            BossAI spawnedMob = Instantiate(bossPrefab, CalculatePosSpawn(), bossPrefab.transform.rotation).GetComponent<BossAI>();
            spawnedMob.setup(pGameData.waveNo);
            spawnedmobList.Add(spawnedMob);
        }

        currentWaveMobQuantity = mobQuantity;

        MobSpawningInformation.MobWaveInfo curWaveInfo = mobSpawningInfo.GetMobWaveInfo(pGameData.waveNo);

        for (int i = 0; i < mobQuantity; i++)
        {


            GameObject mobPrefab = null;
            if (curWaveInfo == null)
            {
                mobPrefab = mobSpawningInfo.GetRandomMob();
            }
            else
            {
                int percent = 100;
                for(int mon = 0; mon < curWaveInfo.monsterSpawnRatio.Length; mon++)
                {
                    float randomVal = Random.Range(0.0f, 1.0f);

                    if (mon == curWaveInfo.monsterSpawnRatio.Length - 1)
                    {
                        mobPrefab = mobSpawningInfo.GetMob((int) curWaveInfo.monsterSpawnRatio[mon].x);
                    }
                    else if(randomVal <= curWaveInfo.monsterSpawnRatio[mon].y/percent)
                    {
                        mobPrefab = mobSpawningInfo.GetMob((int)curWaveInfo.monsterSpawnRatio[mon].x);
                        break;
                    }
                    else
                    {
                        percent -= (int) curWaveInfo.monsterSpawnRatio[mon].y;
                    }
                }
            }

            AIController spawnedMob = Instantiate(mobPrefab, CalculatePosSpawn(), mobPrefab.transform.rotation).GetComponent<AIController>();
            spawnedMob.setup(pGameData.waveNo);
            spawnedmobList.Add(spawnedMob);
        }

        waveStarted = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        { 
            UIManager.Instance.ToggleUpgradePanel();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.Instance.ToggleEscapePanel();
        }

        if (!gamePaused && waveStarted)
        {
            bool isDone = true;
            int mobCount = 0;

            foreach(AIController mob in spawnedmobList)
            {
                if(!mob.isDead())
                {
                    isDone = false;
                    mobCount++;
                }
            }

            UIManager.Instance.SetMobNoText(mobCount);

            if (isDone) //EndOfWaveCalls
            {
                waveStarted = false;
                UIManager.Instance.EndOfWaveAction();
                pGameData.waveNo += 1;
                UIManager.Instance.SetWaveStatus(pGameData.waveNo);
                player.reset();

                if (autoWaveStart)
                {
                    UIManager.Instance.StartWaveAction();
                }
            }
        }
    }


    #region tempHack
    public void increaseWave()
    {
        pGameData.waveNo++;
        UIManager.Instance.SetWaveStatus(pGameData.waveNo);
    }
    #endregion

    public void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
    }
    public void UnpauseGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
    }


    public void SetAutoWaveStatus(bool status)
    {
        autoWaveStart = status;
    }
}
