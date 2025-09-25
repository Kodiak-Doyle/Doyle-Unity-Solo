using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private GameObject pauseMenu;
    public bool isPaused = false;

    private ShotController shotCon;

    void Start()
    {
        shotCon = GameObject.FindGameObjectWithTag("shotControl").GetComponent<ShotController>();

        if (SceneManager.GetActiveScene().buildIndex >= 0)
        {
            pauseMenu = GameObject.FindGameObjectWithTag("Pause");
            pauseMenu.SetActive(false);
        }
    }

    public void Pause()
    {
        if (!isPaused)
        {
            isPaused = true;

            pauseMenu.SetActive(true);

            Time.timeScale = 0;

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            if (!shotCon.isReloading)
            {
                shotCon.canShoot = false;
            }
        }
        else
        {
            Resume();
        }
    }

    public void Resume()
    {
        if (isPaused)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if (!shotCon.isReloading)
            {
                shotCon.canShoot = true;
            }
        }
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(int level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
    }

    public void MainMenu()
    {
        LoadLevel(0);
    }
}
