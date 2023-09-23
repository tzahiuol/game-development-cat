﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireControl : MonoBehaviour
{
    //audio source 
    [SerializeField]
    private AudioSource splashSound;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other){
        Debug.Log(other.gameObject.tag);
        if(other.gameObject.tag == "shoveItem"){
            splashSound.Play();
            Destroy(gameObject);
        }
    }
}
