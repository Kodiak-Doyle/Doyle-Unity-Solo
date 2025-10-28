using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    private GameObject pauseMenu;
    public bool isPaused = false;
    private GameObject LoseScreen;
    private bool isDead = false;
    private GameObject WinScreen;
    private GameObject CrossHair;

    public int currentLevel;

    private ShotController shotCon;
    private PlayerController Pc;
    public Stopwatch stopwatch;
    private SceneMaster SM;


    void Start()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;

        Pc = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        shotCon = GameObject.FindGameObjectWithTag("shotControl").GetComponent<ShotController>();
        LoseScreen = GameObject.FindGameObjectWithTag("LoseScreen");
        CrossHair = GameObject.FindGameObjectWithTag("CrossHair");
        WinScreen = GameObject.FindGameObjectWithTag("WinScreen");
        pauseMenu = GameObject.FindGameObjectWithTag("Pause");
        stopwatch = GameObject.FindGameObjectWithTag("Stopwatch").GetComponent<Stopwatch>();
        SM = GameObject.FindGameObjectWithTag("SceneMaster").GetComponent<SceneMaster>();

        //Debug.Log("found PAuse");
        LoseScreen.SetActive(false);
        WinScreen.SetActive(false);
        pauseMenu.SetActive(false);

        if (currentLevel == 0)
        {
            Time.timeScale = 0;
        }
        if (currentLevel != 0)
        {
            Time.timeScale = 1;
        }

    }

    private void Update()
    {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public void Lose()
    {
        LoseScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        shotCon.canShoot = false;
        isDead = true;
        CrossHair.SetActive(false);
    }

    public void Win()
    {
        WinScreen.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        shotCon.canShoot = false;
        CrossHair.SetActive(false);

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
            CrossHair.SetActive(false);

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
            CrossHair.SetActive(true);

            if (!shotCon.isReloading)
            {
                shotCon.canShoot = true;
            }
        }
        if (isDead)
        {
            isDead = false;
            LoseScreen.SetActive(false);
            Time.timeScale = 1;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            CrossHair.SetActive(true);

            if (!shotCon.isReloading)
            {
                shotCon.canShoot = true;
            }
            Pc.Death();
        }
    }

    public void RestartLevel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(currentLevel);
    }

    public void FullRestart()
    {
        SM.fullReset();
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
        stopwatch.ResetStopwatch();
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void LoadLevel(int level)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(level);
        currentLevel = level;
    }

    public void MainMenu()
    {
        LoadLevel(0);
    }

}
