using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.VisualScripting;

public class SpeedBoostTimerUI : MonoBehaviour
{
    [Header("UI")]
    public Image boostCircle;

    // Add child icon image here
    public Image boostIcon;

    [Header("Timer")]
    public float maxTime = 10f;
    public float timeLeft;

    private bool isRunning;

    void Start()
    {
        boostCircle.fillAmount = 0;
        boostIcon.fillAmount = 0;

        // Hide both at start
        boostCircle.gameObject.SetActive(false);
        boostIcon.gameObject.SetActive(false);
    }

    public void StartBoost()
    {
        timeLeft = maxTime;
        isRunning = true;

        // Show both
        boostCircle.gameObject.SetActive(true);
        boostIcon.gameObject.SetActive(true);
    }

    void Update()
    {
        if (!isRunning) return;

        timeLeft -= Time.deltaTime;

        float fill = timeLeft / maxTime;
        fill = Mathf.Clamp01(fill);

        // Update BOTH radial fills
        boostCircle.fillAmount = fill;
        boostIcon.fillAmount = fill;

        if (timeLeft <= 0)
        {
            EndBoost();
        }
    }

    void EndBoost()
    {
        isRunning = false;

        // Hide both
        boostCircle.gameObject.SetActive(false);
        boostIcon.gameObject.SetActive(false);

        boostCircle.fillAmount = 0;
        boostIcon.fillAmount = 0;
    }
}