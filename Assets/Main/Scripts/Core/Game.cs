using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static bool isPaused;
    public static int score;
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

        if (CharacterMaster.instance != null && CharacterMaster.instance.Health.isDead)
            GameOver();
    }

    public void Play()
    {
        if (CharacterMaster.instance != null && CharacterMaster.instance.Health.isDead)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
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

    public void GameOver()
    {
        Text title = menu.GetComponentInChildren<Text>();
        title.color = Color.red;
        title.text = "D  E  A  D";
        Pause();
    }

    public static void AddScore(int scoreDelta)
    {
        score += scoreDelta;
    }
}
