using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public float speed = 5f;

    private static bool collisionsEnabled = true;
    private Collider2D col;

    void Start()
    {
        col = GetComponent<Collider2D>();
    }

    void Update()
    {
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        float destroyX = Camera.main.transform.position.x - 15f;

        if (transform.position.x < destroyX)
        {
            Destroy(gameObject);
        }
    }

    public static void SetCollisions(bool enabled)
    {
        collisionsEnabled = enabled;

        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (Obstacle o in obstacles)
        {
            if (o.col != null)
                o.col.enabled = enabled;
        }
    }
}