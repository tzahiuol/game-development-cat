<<<<<<< Updated upstream
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float TimeLeft;
    public bool TimerOn = false;

    public Text TimerText;


    // Start is called before the first frame update
    void Start()
    {
        TimerOn = true;
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
            Debug.Log(string.Format("Timer left {0}",TimeLeft));
        }
        
    }

    void UpdateText(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        string newText = string.Format("{0:00}:{1:00}", minutes, seconds);
        TimerText.text = newText;
        Debug.Log(string.Format("Updated string to: {0}", newText));

    }
}
=======
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

    public float TimeLeft;
    public bool TimerOn = false;

    public Text TimerText;


    // Start is called before the first frame update
    void Start()
    {
        TimerOn = true;
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
            Debug.Log(string.Format("Timer left {0}",TimeLeft));
        }
        
    }

    void UpdateText(float currentTime)
    {
        currentTime += 1;

        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        string newText = string.Format("{0:00}:{1:00}", minutes, seconds);
        TimerText.text = newText;
        Debug.Log(string.Format("Updated string to: {0}", newText));

    }
}
>>>>>>> Stashed changes
