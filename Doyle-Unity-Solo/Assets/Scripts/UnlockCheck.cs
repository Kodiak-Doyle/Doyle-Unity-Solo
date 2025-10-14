using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UnlockCheck : MonoBehaviour
{
    private static UnlockCheck instance;

    public bool unlocked;
    public GameObject lvl2Unlock;


    private string levelID;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void Start()
    {
        lvl2Unlock = GameObject.FindGameObjectWithTag("lvl2Unlock");
        unlocked = false;
    }

    void Update()
    {
        Scene scene = SceneManager.GetActiveScene();
        levelID = scene.name;
        //lvl2Unlock = GameObject.FindGameObjectWithTag("lvl2Unlock");


        if (levelID == "Level2")
        {
            unlocked = true;
        }

        if(levelID == "MainMenu" && unlocked)
        {
            lvl2Unlock.SetActive(true);
            Debug.Log("unlocked");
        }
        else
        {
            lvl2Unlock.SetActive(false);
        }

    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        lvl2Unlock = GameObject.FindGameObjectWithTag("lvl2Unlock");
    }
}
