
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float InitalTime;

    private float TimeLeft;
    public bool TimerOn = false;

    public Text TimerText;

    private static Timer _instance;

    public static Timer Instance { get { return _instance; } }


    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TimeLeft = InitalTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                UpdateText(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                TimerOn = false;
            }
        }
        
    }

    public void Restart()
    {
        print("Restarting");
        TimeLeft = InitalTime;
    }

    void UpdateText(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        string newText = string.Format("{0:00}:{1:00}", minutes, seconds);
        TimerText.text = newText;

    }
}
