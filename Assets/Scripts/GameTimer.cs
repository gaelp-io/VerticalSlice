using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameTimer : MonoBehaviour
{
    public static List<GameTimer> allTimers = new List<GameTimer>();

    public TMP_Text timerText;
    public TMP_Text timerShadowText;

    private float timer = 0f;
    private bool isRunning = true;

    void Awake()
    {
        allTimers.Add(this);
    }

    void OnDestroy()
    {
        allTimers.Remove(this);
    }

    void Update()
    {
        if (!isRunning) return;

        timer += Time.deltaTime;

        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        int milliseconds = Mathf.FloorToInt((timer * 100f) % 100f);

        string timeString = string.Format(
            "Time: {0:00}:{1:00}:{2:00}",
            minutes,
            seconds,
            milliseconds
        );

        timerText.text = timeString;
        timerShadowText.text = timeString;
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