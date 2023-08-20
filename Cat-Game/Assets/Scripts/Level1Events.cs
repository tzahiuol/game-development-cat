using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Level1Events : MonoBehaviour
{
    public RotateWorld camCorner;
    public GameObject bathroom;

    // Update is called once per frame
    void Update()
    {
        if (camCorner.bathRoomCorner)
        {
            bathroom.SetActive(false);
        }
        else
        {
            
            bathroom.SetActive(true);
        }
    }
}
