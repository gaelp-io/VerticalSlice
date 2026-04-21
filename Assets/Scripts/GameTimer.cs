using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public TMP_Text timerText;

    private float timer = 0f;
    private bool isRunning = true;

    void Update()
    {
        if (!isRunning) return;

        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);

        // 2-digit milliseconds (00 - 99)
        int milliseconds = Mathf.FloorToInt((timer * 100f) % 100f);

        timerText.text = string.Format(
            "Time: {0:00}:{1:00}:{2:00}",
            minutes,
            seconds,
            milliseconds
        );
    }

    public void StopTimer()
    {
        isRunning = false;
    }

    public void ResetTimer()
    {
        timer = 0f;
        isRunning = true;
    }

    public float GetTime()
    {
        return timer;
    }
}