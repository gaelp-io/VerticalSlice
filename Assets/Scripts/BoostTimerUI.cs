using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedBoostTimerUI : MonoBehaviour
{
    [Header("UI")]
    public Image boostCircle;

    [Header("Timer")]
    public float maxTime = 10f;
    public float timeLeft;

    private bool isRunning;

    void Start()
    {
        boostCircle.fillAmount = 0;
    }

    public void StartBoost()
    {
        timeLeft = maxTime;
        isRunning = true;
        boostCircle.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!isRunning) return;

        timeLeft -= Time.deltaTime;

        float fill = timeLeft / maxTime;
        boostCircle.fillAmount = Mathf.Clamp01(fill);

        if (timeLeft <= 0)
        {
            EndBoost();
        }
    }

    void EndBoost()
    {
        isRunning = false;
        boostCircle.gameObject.SetActive(false);
        boostCircle.fillAmount = 0;
    }
}