using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    public static bool isPaused;
    [SerializeField] private GameObject hud = null;
    [SerializeField] private GameObject menu = null;

    void Start()
    {
        isPaused = true;
        hud.SetActive(false);
        Time.timeScale = 0.0f;
    }

    private void Update()
    {
        if (Input.GetKeyDown("escape") || Input.GetKeyDown("p"))
        {
            Pause();
        }
    }

    public void Play()
    {
        isPaused = false;
        Time.timeScale = 1.0f;
        menu.SetActive(false);
        hud.SetActive(true);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        menu.SetActive(true);
        hud.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        isPaused = true;
        Time.timeScale = 0.0f;
    }
}
