
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

    public static float TimeLeft_Static;

    // Start is called before the first frame update
    private void Start()
    {
        if (Timer.TimeLeft_Static == 0)
        {
            TimeLeft = InitalTime; 
        }
        else
        {
            TimeLeft = Timer.TimeLeft_Static;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                Timer.TimeLeft_Static = TimeLeft;

                UpdateText(TimeLeft);
            }
            else
            {
                TimeLeft = 0;
                Timer.TimeLeft_Static = TimeLeft;
                TimerOn = false;
                TimeIsUp();
            }
        }
        
    }

    private void TimeIsUp()
    {
        ColliosionHandler ch = FindObjectOfType<ColliosionHandler>();
        ch.EndGame(true);
    }

    public void Restart()
    {
        print("Restarting");
        TimeLeft = InitalTime;
        Timer.TimeLeft_Static = InitalTime;
        TimerOn = true;
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
