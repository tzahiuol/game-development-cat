﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        ColliosionHandler colliosionHandler = GameObject.FindObjectOfType<ColliosionHandler>();
        GameObject gameOverPanel = GameObject.Find("GameOver");
        gameOverPanel.SetActive(false);
        Time.timeScale = 1f;
        colliosionHandler.Restart();
    }
}
