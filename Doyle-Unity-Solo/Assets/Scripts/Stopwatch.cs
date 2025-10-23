using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

public class Stopwatch : MonoBehaviour
{
    private static Stopwatch instance;

    [SerializeField]  private TextMeshProUGUI UiText;

    public bool isRunning = false;

    public float elapsedTime = 0f;

    public bool TextFound = false;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    void Start()
    {
        StartStopwatch();

        //UiText = GameObject.FindGameObjectWithTag("StpTxt");
        //UiText = GameObject.Find("Stopwatch Text").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (!TextFound)
        {
            UiText = GameObject.Find("StopwatchText").GetComponent<TextMeshProUGUI>();
            if (UiText != null)
            {
                TextFound = true;
            }
        }
        
        
            
        
        if (isRunning)
        {
            elapsedTime += Time.deltaTime;
            //TimeSpan time = TimeSpan.FromSeconds(elapsedTime);
            UiText.text = elapsedTime.ToString();
        }
    }

    public void StartStopwatch()
    {
        isRunning = true;
    }

    public void StopStopwatch()
    {
        isRunning = false;
    }   
}
