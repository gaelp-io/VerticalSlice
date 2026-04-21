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

    private bool spawnedBoost = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        if (!spawnedBoost && timer.GetTime() >= 5f)
        {
            Vector3 spawnPos = Camera.main.transform.position + new Vector3(12f, -0.46f, 0f);
            spawnPos.z = 0f;

            Instantiate(boostPrefab, spawnPos, Quaternion.identity);
            spawnedBoost = true;
        }
    }

    public void StartGame()
    {
        timer.ResetTimer();
        spawnedBoost = false;
    }

    public void StopGame()
    {
        timer.StopTimer();
    }
}