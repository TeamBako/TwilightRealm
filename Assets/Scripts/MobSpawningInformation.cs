using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MobSpawningInfo", menuName = "ScriptableObjects/Spawn MobSpawningInfo", order = 1)]
public class MobSpawningInformation : ScriptableObject
{
    public int mobQuantity;
    public int additonalMobPerSession;
    public int mobWavePerSession;

    public GameObject[] allMonsterPrefab;

    public List<MobWaveInfo> mobWaveInfos;

    public int GetNoOfMobToSpawn(int wave)
    {
        return mobQuantity + (additonalMobPerSession * ((wave - 1) / mobWavePerSession));
    }

    public GameObject GetRandomMob()
    {
        return allMonsterPrefab[Random.Range(0, allMonsterPrefab.Length)];
    }

    public GameObject GetMob(int no)
    {
        return allMonsterPrefab[no];
    }

    public bool HasCustomWaveInfo(int waveNo)
    {
        bool haveWaveInfo = false;

        foreach(MobWaveInfo waveInfo in mobWaveInfos)
        {
            if(waveInfo.waveNo == waveNo)
            {
                haveWaveInfo = true;
                break;
            }
        }

        return haveWaveInfo;
    }

    public MobWaveInfo GetMobWaveInfo(int waveNo)
    {
        MobWaveInfo waveInfo = null;

        foreach (MobWaveInfo curWaveInfo in mobWaveInfos)
        {
            if (curWaveInfo.waveNo == waveNo)
            {
                waveInfo = curWaveInfo;
                break;
            }
        }

        return waveInfo;
    }

    [System.Serializable]
    public class MobWaveInfo
    {
        public int waveNo;

        public Vector2[] monsterSpawnRatio;

        public MobWaveInfo()
        {
            waveNo = 0;
        }

        public Vector2[] GetMonsterSpawnRatio()
        {
            return monsterSpawnRatio;
        }
    }
}
