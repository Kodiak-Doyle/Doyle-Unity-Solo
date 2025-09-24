using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMaster : MonoBehaviour
{

    public static SceneMaster active;
    public Checkpoint currentCheckpoint;
    private string levelID;

    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        levelID = scene.name;

        if (SceneMaster.active != null)
        {
            if(SceneMaster.active.levelID != scene.name)
            {
                Destroy(SceneMaster.active.gameObject);
                SceneMaster.active = null;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if(SceneMaster.active == null)
        {
            SceneMaster.active = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
