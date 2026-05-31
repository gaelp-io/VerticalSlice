using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    [Header("UI")]
    public GameObject qtePanel;
    public TMP_Text counterText;
    public TMP_Text timerText;

    [Header("Settings")]
    public int pressesRequired = 20;
    public float qteDuration = 3f;

    private int currentPresses;
    private float timeRemaining;
    private bool qteActive;

    public static QTEManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        qtePanel.SetActive(false);
    }

    public void StartQTE()
    {
        currentPresses = 0;
        timeRemaining = qteDuration;
        qteActive = true;

        qtePanel.SetActive(true);
        UpdateUI();
    }

    void Update()
    {
        if (!qteActive) return;

        timeRemaining -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            currentPresses++;
            UpdateUI();

            if (currentPresses >= pressesRequired)
            {
                Success();
            }
        }

        if (timeRemaining <= 0)
        {
            Failure();
        }

        timerText.text = timeRemaining.ToString("F1");
    }

    void UpdateUI()
    {
        counterText.text =
            currentPresses + " / " + pressesRequired;
    }

    void Success()
    {
        Debug.Log("QTE Success");

        qteActive = false;
        qtePanel.SetActive(false);

        FindObjectOfType<EnemyChase>().EndQTE();
    }

    void Failure()
    {
        Debug.Log("QTE Failed");

        qteActive = false;
        qtePanel.SetActive(false);

        FindObjectOfType<EnemyChase>().EndQTE();
    }
}