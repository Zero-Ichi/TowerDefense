using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Enemy 
    [SerializeField]
    private Transform spawnEnemyPoint;
    [SerializeField]
    private Transform standardEnemyPrefab;

    //Friend 
    [SerializeField]
    private Transform spawnFriendPoint;
    [SerializeField]
    private Transform standardFriendPrefab;

    [SerializeField]
    private float timeBetweenWaves = 5f;

    private float countdown = 5f;

    public int WavesIndex { get; set; }

    private void Awake()
    {
        WavesIndex = 0;
    }

    private void Update()
    {
        if (countdown <= 0)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
    }

    IEnumerator SpawnWave()
    {
        WavesIndex++;
        for (int i = 0; i < WavesIndex; i++)
        {
            SpawnWalker(standardEnemyPrefab,spawnEnemyPoint);
            SpawnWalker(standardFriendPrefab,spawnFriendPoint);
            yield return new WaitForSeconds(0.2f);
        }
    }

    private void SpawnWalker(Transform walker, Transform spawn)
    {
        Instantiate(walker, spawn.position, spawn.rotation);
    }
}
