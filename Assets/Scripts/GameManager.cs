using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameTimer timer;

    [Header("Speed Boost Spawn")]
    public GameObject boostPrefab;
    public Transform spawnPoint;

    private bool spawnedat5 = false;
    private bool spawnedat20 = false;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        float time = timer.GetTime();

        if (!spawnedat5 && time >= 5f)
        {
            SpawnBoost();
            spawnedat5 = true;
        }

        if (!spawnedat20 && time >= 20f)
        {
            SpawnBoost();
            spawnedat20 = true;
        }
    }

    public void SpawnBoost()
    {
        Vector3 spawnPos = Camera.main.transform.position + new Vector3(12f, -0.46f, 0f);
            spawnPos.z = 0f;

            Instantiate(boostPrefab, spawnPos, Quaternion.identity);
            Debug.Log("boost spawned!");
    }

    public void StartGame()
    {
        timer.ResetTimer();
        spawnedat5 = false;
        spawnedat20 = false;
    }

    public void StopGame()
    {
        timer.StopTimer();
    }
}