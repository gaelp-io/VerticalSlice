using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostMover : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float destroyX = -15f;

    void Update()
    {
        transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }
}
