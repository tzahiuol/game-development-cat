using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;
using System;
using TMPro;


public class CameraControl : MonoBehaviour
{
    //public Transform pivotPoint;
    public Transform character;
    //public float rotationSpeed = 20f;
    Transform Obstruction;
    Vector3 offset;

    //Zoom Vector
    //For camera zoom
    Vector3 zoomVector; //defines zoom target
    Vector3 zoomVectorOut; //defines zoom out targer

    public float maxZoomIn = 4f; //to limit the max zoom in
    public float maxZoomOut = 10f; //to limit the max zoom out 

    public float lerpSpeed = 0.002f;


    private void Start()
    {
        Obstruction = character;
        //UoL Lecture 3D Graphics and Animation
        offset = transform.position - character.transform.position;
        transform.LookAt(character.position);
    }

    void Update()
    {
        //zoom in and out
        // check key presses for rotation.
       //transform.LookAt(character.position);
        if (Keyboard.current.kKey.isPressed || Keyboard.current.lKey.isPressed)
        {
            cameraZoom();
        }


        //// follow the character
        Vector3 targetPosition = character.position + offset;
        ////smooth camera transition 
        //transform.position = Vector3.Lerp(transform.position, targetPosition, 0f);
        
        

        //transform.position = Vector3.Lerp(transform.position, targetPosition, 0f);
        ////https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
        transform.LookAt(character.position);
        //Debug.Log("Character " + character.position + " Camera " + transform.position);
        zoomVector = new Vector3(character.position.x, transform.position.y, character.position.z);
        zoomVectorOut = new Vector3(-zoomVector.x, transform.position.y, -zoomVector.z);
    }

        private void LateUpdate()
    {
        ViewObstructed();
    }



   //Handles camera zoom 
    void cameraZoom()
    {
        Debug.Log("Zoomed");
       
        
        //https://learn.microsoft.com/en-us/dotnet/api/system.single.equals?view=net-7.0
        //get absolute difference 
        float diffX = Math.Abs(character.position.x - transform.position.x); 
        float diffZ = Math.Abs(character.position.z - transform.position.z);
        float diffY = Math.Abs(character.position.y - transform.position.y);

        if (Keyboard.current.kKey.isPressed) //Zoom In
            {
                //compare absolute difference with zoom constraint 
                if ((diffX >= maxZoomIn) &&
                (diffZ >= maxZoomIn))
                {
                //transform.LookAt(character.position);
                transform.position = Vector3.Lerp(transform.position, zoomVector, lerpSpeed);
                }

            }
            else if(Keyboard.current.lKey.isPressed) //Zoom Out
            {
            if ((diffX <= maxZoomOut) &&
                (diffZ <= maxZoomOut))
            {
                transform.LookAt(character.position);
                transform.position = Vector3.Lerp(transform.position, zoomVectorOut, lerpSpeed);
                
            }
        }


    }

    void ViewObstructed()
    {       
        RaycastHit hit;        

        if(Physics.Raycast(transform.position, character.position - transform.position, out hit, 100f))
        {

            if(hit.collider.gameObject.tag != "Player")
            {                
                Obstruction = hit.transform;
                if (Obstruction.gameObject.GetComponent<MeshRenderer>())
                    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;

            }
            else
                if(Obstruction.gameObject.GetComponent<MeshRenderer>())
                    Obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;               
        }
    }

}





