using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Enemy")]
    public GameObject enemyPrefab;

    [Header("Positions")]
    public Vector2 spawnPosition;
    public Vector2 resetPosition;

    [Header("Timing")]
    public float firstSpawnTime = 60f;   // 1 minute
    public float spawnInterval = 180f;   // 3 minutes

    public GameTimer timer;

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = firstSpawnTime;
    }

    void Update()
    {
        float currentTime = timer.GetTime();

        if (currentTime >= nextSpawnTime)
        {
            SpawnEnemy();

            nextSpawnTime += spawnInterval;
        }
    }

    void SpawnEnemy()
    {
        GameObject enemy = Instantiate(
            enemyPrefab,
            spawnPosition,
            Quaternion.identity
        );

        EnemyChase.enemyActive = true;

        EnemyChase chase = enemy.GetComponent<EnemyChase>();

        if (chase != null)
        {
            chase.resetPosition = resetPosition;
        }

        Debug.Log("Enemy Spawned!");
    }
}