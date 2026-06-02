using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Death")]
    public float destroyX = 20f;

    [Header("Chase Settings")]
    public float detectionRange = 5f;
    public float moveSpeed = 3f;

    [Header("Reset Position")]
    public Vector2 resetPosition;
    public Vector2 spawnPosition;

    private bool isReturning = false;

    [Header("Lives")]
    public int lives = 3;

    private bool isDying = false;
    private bool drivingAway = false;

    private Rigidbody2D rb;
    private bool enteringScene = true;
    private bool isChasing;
    private bool qteTriggered = false;
    private bool waitingForQTE = false;
    private Collider2D enemyCollider;
    public static bool enemyActive = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
                player = playerObj.transform;
        }

        enteringScene = true;

        enemyCollider = GetComponent<Collider2D>();
        enemyCollider.enabled = false;
    }

    void FixedUpdate()
    {
        if (enteringScene)
        {
            Vector2 direction =
                (resetPosition - rb.position).normalized;

            rb.velocity = direction * moveSpeed;

            float distance =
                Vector2.Distance(rb.position, resetPosition);

            if (distance < 0.1f)
            {
                rb.velocity = Vector2.zero;
                transform.position = resetPosition;
                enemyCollider.enabled = true;
                enteringScene = false;
            }

            return;
        }

        if (drivingAway)
        {
            /*Debug.Log("Driving away. X = " + transform.position.x);*/

            rb.velocity = Vector2.right * moveSpeed;

            if (transform.position.x >= destroyX)
            {
                Debug.Log("Destroying enemy");
                EnemyChase.enemyActive = false;
                Destroy(gameObject);
            }

            return;
        }

        // Return to reset point
        if (isReturning)
        {
            Vector2 direction =
                (resetPosition - rb.position).normalized;

            rb.velocity = direction * moveSpeed;

            float distanceToReset =
                Vector2.Distance(transform.position, resetPosition);

            if (distanceToReset < 0.1f)
            {
                transform.position = resetPosition;

                if (isDying)
                {
                    /*Debug.Log("Enemy reached reset point and is driving away");*/

                    drivingAway = true;
                    isReturning = false;
                    return;
                }

                rb.velocity = Vector2.zero;
                isReturning = false;

                // Stay parked here while QTE is active
                if (waitingForQTE)
                {
                    return;
                }
            }

            return;
        }

        if (waitingForQTE && !isReturning)
        {
            rb.velocity = Vector2.zero;
            return;
        }

        // Normal chase behavior
        float distanceToPlayer =
            Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRange)
        {
            Vector2 direction =
                ((Vector2)player.position - rb.position).normalized;

            rb.velocity = direction * moveSpeed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Enemy touched player!");

            isReturning = true;
            waitingForQTE = true;

            QTEManager.Instance.StartQTE(this);
        }
    }

    public void EndQTE()
    {
        waitingForQTE = false;
    }

    public void TakeQTEDamage()
    {
        lives--;

        Debug.Log("Enemy lives remaining: " + lives);

        if (lives <= 0)
        {
            isDying = true;
            isReturning = true;

            Collider2D col = GetComponent<Collider2D>();
            if (col != null)
            {
                col.enabled = false;
            }
        }
    }
}