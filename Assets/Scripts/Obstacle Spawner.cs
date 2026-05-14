using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;

    public GameTimer timer;
    private bool startedSpawning = false;

    [Header("Spawn Settings")]
    public float spawnInterval = 2f;
    public float spawnDistance = 12f;
    private int lastLane = -1;

    [Header("Lane Positions (Y values)")]
    public float[] laneYPositions = new float[3]; // assign in inspector

    [Header("Obstacle Colors")]
    public Color[] obstacleColors;

    void Update()
    {
        float time = timer.GetTime();

        if (!startedSpawning && time >= 6f)
        {
            startedSpawning = true;
            StartCoroutine(SpawnRoutine());
        }
    }

    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            SpawnObstacle();
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    void SpawnObstacle()
    {
        int laneIndex;

        do
        {
            laneIndex = Random.Range(0, laneYPositions.Length);
        }
        while (laneIndex == lastLane);

        lastLane = laneIndex;

        float yPos = laneYPositions[laneIndex];

        Vector3 spawnPos = Camera.main.transform.position + new Vector3(spawnDistance, 0f, 0f);
        spawnPos.y = yPos;
        spawnPos.z = 0f;

        Collider2D[] hits = Physics2D.OverlapCircleAll(spawnPos, 1.5f);

        foreach (Collider2D hit in hits)
        {
            if (hit.CompareTag("speedboost"))
            {
                return; // cancel obstacle spawn
            }
        }

        GameObject obstacle = Instantiate(obstaclePrefab, spawnPos, Quaternion.identity);

        obstacle.layer = LayerMask.NameToLayer("Obstacle");

        SpriteRenderer sr = obstacle.GetComponent<SpriteRenderer>();

        if (sr != null && obstacleColors.Length > 0)
        {
            Color c = obstacleColors[Random.Range(0, obstacleColors.Length)];
            c.a = 1f;
            sr.color = c;
        }
    }
}