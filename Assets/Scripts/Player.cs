using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoostTrigger : MonoBehaviour
{
    public SpeedBoostTimerUI boostUI;

    [Header("Lives")]
    public int lives = 3;
    public float invincibilityTime = 5f;

    private bool isInvincible = false;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            TakeDamage();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("speedboost"))
        {
            boostUI.StartBoost();

            Destroy(other.gameObject);
        }
    }

    void TakeDamage()
    {
        if (isInvincible) return;

        lives--;

        if (lives <= 0)
        {
            Destroy(gameObject);
            GameManager.Instance.StopGame();
            GameManager.Instance.StopAllTimers();
            GameManager.Instance.StopGame();
            return;
        }

        StartCoroutine(InvincibilityRoutine());
    }

    IEnumerator InvincibilityRoutine()
    {
        isInvincible = true;

        // make player invisible
        Color c = sr.color;
        c.a = 0.3f;
        sr.color = c;

        // disable obstacle collisions
        Obstacle.SetCollisions(false);

        yield return new WaitForSeconds(invincibilityTime);

        // restore player
        c.a = 1f;
        sr.color = c;

        Obstacle.SetCollisions(true);

        isInvincible = false;
    }
}