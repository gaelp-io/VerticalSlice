using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerBoostTrigger : MonoBehaviour
{
    public SpeedBoostTimerUI boostUI;

    [Header("Lives")]
    public TMP_Text livesText;
    public TMP_Text livesShadowText;
    public int lives = 3;
    public float invincibilityTime = 5f;

    private bool isInvincible = false;
    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        UpdateLivesUI();
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

    void UpdateLivesUI()
    {
        string text = "Lives: " + lives;

        livesText.text = text;
        livesShadowText.text = text;
    }

    void TakeDamage()
    {
        if (isInvincible) return;

        lives--;
        UpdateLivesUI();

        if (lives <= 0)
        {
            Destroy(gameObject);
            
            GameManager.Instance.finalTime = GameManager.Instance.timer.GetTime();
            GameManager.Instance.gameEnded = true;

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

        // disable obstacle collisions immediately
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Obstacle"),
            true
        );

        float elapsed = 0f;
        float blinkRate = 0.15f;

        while (elapsed < invincibilityTime)
        {
            bool visible = Mathf.FloorToInt(Time.time / blinkRate) % 2 == 0;

            if (visible)
            {
                sr.color = new Color(1f, 0f, 0f, 0.5f); // red + transparent
            }
            else
            {
                sr.color = new Color(1f, 1f, 1f, 0.2f); // faint white
            }

            elapsed += Time.deltaTime;
            yield return null;
        }

        // restore collisions FIRST
        Physics2D.IgnoreLayerCollision(
            LayerMask.NameToLayer("Player"),
            LayerMask.NameToLayer("Obstacle"),
            false
        );

        // restore visuals
        sr.color = Color.white;
        isInvincible = false;
    }

    public void ActivateBoostUI()
    {
        Debug.Log("BOOST UI METHOD CALLED");
        boostUI.StartBoost();
    }
}