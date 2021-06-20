using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SystemManager : MonoBehaviour
{

    public static SystemManager Instance;

    private string playerFileName = "PlayerInfo";
    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        activatePlayer();
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

    // Update is called once per frame
    void Update()
    {
        
    }

    private void savePlayerProgress()
    {
        SerializeManager.Save(playerFileName, PlayerControl.Instance.deactivate());
    }

    private void OnDestroy()
    {
        savePlayerProgress();
    }

}
