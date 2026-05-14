using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameTimer timer;
    public float finalTime;
    public bool gameEnded = false;

    public GameObject gameOverUI;
    public TMP_Text finalTimeText;

    [Header("Speed Boost Spawn")]
    public GameObject boostPrefab;
    public Transform spawnPoint;

    private bool spawnedat5 = false;
    private float nextBoostTime = 25f;

    public void StartGame()
    {
        timer.ResetTimer();
        spawnedat5 = false;
        nextBoostTime = 25f;
    }

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

        if (time >= nextBoostTime)
        {
            SpawnBoost();

            // next boost spawns 20–35 seconds later
            nextBoostTime = time + Random.Range(20f, 35f);
        }

        if (gameEnded)
        {
            gameOverUI.SetActive(true);

            float t = finalTime;

            int minutes = Mathf.FloorToInt(t / 60f);
            int seconds = Mathf.FloorToInt(t % 60f);
            int milliseconds = Mathf.FloorToInt((t * 100f) % 100f);

            finalTimeText.text = string.Format(
                "Final Time: {0:00}:{1:00}:{2:00}\nBetter luck next time!",
                minutes,
                seconds,
                milliseconds
            );

            gameEnded = false; // prevent spam
        }
    }

    public void SpawnBoost()
    {
        Vector3 spawnPos = Camera.main.transform.position + new Vector3(12f, -0.46f, 0f);
            spawnPos.z = 0f;

            Collider2D[] hits = Physics2D.OverlapCircleAll(spawnPos, 1.5f);

            foreach (Collider2D hit in hits)
            {
                if (hit.CompareTag("obstacle"))
                {
                    Destroy(hit.gameObject);
                }
            }

            Instantiate(boostPrefab, spawnPos, Quaternion.identity);
            Debug.Log("boost spawned!");
    }

    public void StopGame()
    {
        timer.StopTimer();
    }

    public void StopAllTimers()
    {
        foreach (GameTimer t in GameTimer.allTimers)
        {
            t.StopTimer();
        }
    }
}