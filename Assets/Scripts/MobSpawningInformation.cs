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

    public int GetNoOfMobToSpawn(int wave)
    {
        return mobQuantity + (additonalMobPerSession * ((wave - 1) / mobWavePerSession));
    }

    public GameObject GetRandomMob()
    {
        return allMonsterPrefab[Random.Range(0, allMonsterPrefab.Length)];
    }
}
