using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilemapScroller : MonoBehaviour
{
    public Transform[] chunks;
    public float scrollSpeed = 2f;
    public float chunkWidth = 20f; // set to your tilemap width

    void Update()
    {
        for (int i = 0; i < chunks.Length; i++)
        {
            chunks[i].position += Vector3.left * scrollSpeed * Time.deltaTime;

            // If chunk is off-screen to the left, move it to the right end
            if (chunks[i].position.x <= -chunkWidth)
            {
                float rightMostX = GetRightMostX();
                chunks[i].position = new Vector3(rightMostX + chunkWidth, chunks[i].position.y, 0);
            }
        }
    }

    float GetRightMostX()
    {
        float maxX = float.MinValue;

        foreach (Transform chunk in chunks)
        {
            if (chunk.position.x > maxX)
                maxX = chunk.position.x;
        }

        return maxX;
    }
}