using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [Header("Target")]
    public Transform player;

    [Header("Chase Settings")]
    public float detectionRange = 5f;
    public float moveSpeed = 3f;

    [Header("Reset Position")]
    public Vector2 resetPosition;

    private bool isReturning = false;

    private Rigidbody2D rb;
    private bool isChasing;
    private bool qteTriggered = false;
    private bool waitingForQTE = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

            if (playerObj != null)
                player = playerObj.transform;
        }
    }

    void FixedUpdate()
    {
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
                rb.velocity = Vector2.zero;
                transform.position = resetPosition;

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

            QTEManager.Instance.StartQTE();
        }
    }

    public void EndQTE()
    {
        waitingForQTE = false;
    }
}