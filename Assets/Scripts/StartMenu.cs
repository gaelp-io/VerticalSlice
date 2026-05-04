using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private bool started = false;

    void Update()
    {
        if (started) return;

        // Keyboard support (kept)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartGame();
        }

        // Mouse click anywhere (WebGL friendly)
        if (Input.GetMouseButtonDown(0))
        {
            StartGame();
        }
    }

    void StartGame()
    {
        started = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("GameScene");
    }
}