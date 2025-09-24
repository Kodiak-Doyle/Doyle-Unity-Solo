using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager active;
    private string levelID;

    public string LevelID { get { return levelID; } }

    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();

        if (CheckpointManager.active != null)
        {
            if (CheckpointManager.active.LevelID != scene.name)
            {
                Destroy(CheckpointManager.active.gameObject);
                CheckpointManager.active = null;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        if (CheckpointManager.active == null)
        {
            CheckpointManager.active = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
