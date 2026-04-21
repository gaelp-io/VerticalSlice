using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

public class SpeedBoostPickup : MonoBehaviour
{
    public float speedIncrease = 3f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            float currentSpeed = Variables.Object(other.gameObject).Get<float>("Speed");

            Variables.Object(other.gameObject).Set(
                "Speed",
                currentSpeed + speedIncrease
            );

            Destroy(gameObject);
        }
    }
}