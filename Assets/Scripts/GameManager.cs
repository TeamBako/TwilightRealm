using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public Vector3 spawnLocationBounds;

    private GameData pGameData;

    [SerializeField]
    private MobSpawningInformation mobSpawningInfo;

    // Radius away from player
    private float spawningRadius = 20;
    private PlayerControl player;

    [SerializeField]
    private bool gamePaused = false;

    [SerializeField]
    private bool waveStarted = false;
    private int currentWaveMobQuantity = 0;

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
    }

    public void activate(GameData data)
    {
        pGameData = data;
        UIManager.Instance.SetWaveStatus(pGameData.waveNo);
    }

    public GameData deactivate()
    {
        return pGameData;
    }

    public void ResetGame()
    {
        SystemManager.Instance.ResetPlayerData();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartWave()
    {
        if(spawnedmobList.Count > 0)
        {
            foreach(AIController mob in spawnedmobList)
            {
                Destroy(mob.gameObject);
            }

            spawnedmobList = new List<AIController>();
        }

        int mobQuantity = mobSpawningInfo.GetNoOfMobToSpawn(pGameData.waveNo);

        Vector3 playerPostion = player.GetComponent<Transform>().position;

        currentWaveMobQuantity = mobQuantity;

        for (int i = 0; i < mobQuantity; i++)
        {
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


            GameObject mobPrefab = mobSpawningInfo.GetRandomMob();
            GameObject spawnedMob = Instantiate(mobPrefab, spawnPosition, mobPrefab.transform.rotation);
            spawnedmobList.Add(spawnedMob.GetComponent<AIController>());
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

        if (waveStarted)
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
            }
        }
    }

    public void TogglePauseGame()
    {
        Time.timeScale = gamePaused ? 1 : 0;
        gamePaused = !gamePaused;
    }
}
