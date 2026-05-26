using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    private bool started = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (started) return;

        // Keyboard support (kept)
        if (Input.GetKeyDown(KeyCode.R))
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