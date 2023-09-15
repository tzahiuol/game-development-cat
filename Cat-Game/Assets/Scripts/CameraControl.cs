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

    //a pivot point that the camera moves towards when the zoom out key is pressed
    private GameObject zoomOutPivot;


    Transform Obstruction;
    Vector3 offset;
    Vector3 zoomOutPivotStart;

    //Zoom Vector
    //For camera zoom
    Vector3 zoomVector; //defines zoom target
    Vector3 zoomVectorOut; //defines zoom out targer

    public float maxZoomIn = 2f; //to limit the max zoom in
    public float maxZoomOut = 20f; //to limit the max zoom out 

    public float lerpSpeed = 0.012f;


    private void Start()
    {
        Obstruction = character;
       

        //UoL Lecture 3D Graphics and Animation
        offset = transform.position - character.transform.position;

        //initiate zoom pivot
        zoomOutPivot = new GameObject("ZoomPivot");
        zoomOutPivot.transform.position = this.transform.position + offset;
        zoomOutPivotStart = zoomOutPivot.transform.position;
        

        transform.LookAt(character.position);
    }

    void Update()
    {

       
        //zoom in and out
        // check key presses for rotation.
        //transform.LookAt(character.position);
        if (Keyboard.current.wKey.isPressed || Keyboard.current.sKey.isPressed)
        {
            cameraZoom();
            zoomOutPivot.transform.position = this.transform.position + offset + (this.transform.position - character.transform.position);
            zoomOutPivot.transform.rotation = this.transform.rotation;
            zoomVector = new Vector3(character.position.x, character.position.y, character.position.z);
            zoomVectorOut = new Vector3(zoomOutPivot.transform.position.x, zoomOutPivot.transform.position.y, zoomOutPivot.transform.position.z);
        }
        else
        {
            //// follow the character
            Vector3 targetPosition = character.position + offset;
            ////smooth camera transition 
            //transform.position = Vector3.Lerp(transform.position, targetPosition, 0f);



            //transform.position = Vector3.Lerp(transform.position, targetPosition, 0f);
            ////https://docs.unity3d.com/ScriptReference/Transform.LookAt.html
            transform.LookAt(character.position);
        }


        
        
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

        if (Keyboard.current.wKey.isPressed) //Zoom In
            {
                //compare absolute difference with zoom constraint 
                if ((diffX >= maxZoomIn) &&
                (diffZ >= maxZoomIn))
                {
                //transform.LookAt(character.position);
                transform.position = Vector3.Lerp(transform.position, zoomVector, lerpSpeed);
                }

            }
            else if(Keyboard.current.sKey.isPressed) //Zoom Out
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





