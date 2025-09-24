using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Checkpoint : MonoBehaviour
{
    private bool isCurrentCheckpoint;

    public delegate void UpdateCheckpointDel(string cpName);
    public static event UpdateCheckpointDel OnCheckpointHit;

    private void OnEnable()
    {
        OnCheckpointHit += UpdateCheckpoint;
    }
    private void OnDisable()
    {
        OnCheckpointHit -= UpdateCheckpoint;
    }

    public void UpdateCheckpoint(string cpName)
    {
        if (gameObject.name == cpName)
        {
            SceneMaster.active.currentCheckpoint = this;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!isCurrentCheckpoint)
        {
            if(other.gameObject.tag == "Player")
            {
                Debug.Log("Checkpoint hit" + gameObject.name);
                Checkpoint.OnCheckpointHit(gameObject.name);
            }
        }
    }
}
