using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;
using TMPro;
using UnityEngine.SceneManagement;

public class Stopwatch : MonoBehaviour
{
    private static Stopwatch instance;

    [SerializeField]  private TextMeshProUGUI UiText;

    public bool isRunning = false;

    public float elapsedTime = 0f;

    public bool TextFound = false;

    public float forceUpdateTimer = 0f;

    void Awake()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        } else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartStopwatch();
    }

    void Update()
    {
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
            UiText.text = string.Format("{0:D2}:{1:D2}.{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }
    }

    public void ResetStopwatch()
    {
        elapsedTime = 0f;
        UiText.text = elapsedTime.ToString();
    }

    public void StartStopwatch()
    {
        isRunning = true;
    }

    public void StopStopwatch()
    {
        isRunning = false;
    }   

   private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StartCoroutine(DelayUpdate());
    }

    private IEnumerator DelayUpdate() 
    {
       GameObject uiObject = null;

        while (uiObject == null)
        {
            uiObject = GameObject.Find("StopwatchText");
            yield return null;
        }

        UiText = uiObject.GetComponent<TextMeshProUGUI>();
        TimeSpan timeSpan = TimeSpan.FromSeconds(elapsedTime);
        UiText.text = string.Format("{0:D2}:{1:D2}.{2:D3}", timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
    }

}
