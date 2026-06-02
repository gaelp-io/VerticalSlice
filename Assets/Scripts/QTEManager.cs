using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class QTEManager : MonoBehaviour
{
    private PlayerBoostTrigger playerScript;
    private ScriptMachine machine;
    private GameObject player;
    private EnemyChase currentEnemy;

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
        player = GameObject.FindGameObjectWithTag("Player");

        if (player != null)
        {
            machine = player.GetComponent<ScriptMachine>();
            playerScript = player.GetComponent<PlayerBoostTrigger>();
        }

        qtePanel.SetActive(false);
    }

    public void StartQTE(EnemyChase enemy)
    {
        currentEnemy = enemy;

        machine.enabled = false;

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

        machine.enabled = true;

        if (currentEnemy != null)
        {
            currentEnemy.TakeQTEDamage();
            currentEnemy.EndQTE();
        }
    }

    void Failure()
    {
        Debug.Log("QTE Failed");

        qteActive = false;
        qtePanel.SetActive(false);

        machine.enabled = true;

        if (playerScript != null)
        {
            playerScript.TakeQTEDamage();
        }

        if (currentEnemy != null)
        {
            currentEnemy.EndQTE();
        }
    }
}