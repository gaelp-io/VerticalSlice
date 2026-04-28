using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBoostTrigger : MonoBehaviour
{
    public SpeedBoostTimerUI boostUI;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("speedboost"))
        {
            boostUI.StartBoost();
            Destroy(other.gameObject);
        }
    }
}