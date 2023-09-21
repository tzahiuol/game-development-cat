using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraRotation : MonoBehaviour
{

    public Transform pivotPoint;
    public float rotationSpeed = 40f;

    private Vector3 pivotOffset;


    // Start is called before the first frame update
    void Start()
    {
        pivotOffset = transform.position - pivotPoint.position;
    }

    // Update is called once per frame
    void Update()
    {
        float rotationAmount = 1f;


        //https://docs.unity3d.com/Packages/com.unity.inputsystem@1.0/manual/Keyboard.html
        // check key presses for rotation.
        if (Keyboard.current.dKey.isPressed) //right rotation
        {
            // rotate around the pivot point.
            RotateAroundPivot(-rotationAmount);
        }
        else if (Keyboard.current.aKey.isPressed) //left rotation
        {
            // rotate around the pivot point.
            RotateAroundPivot(rotationAmount);
        }

    }

    void RotateAroundPivot(float rotationAmount)
    {
        Vector3 directionToCamera = transform.position - pivotPoint.position;
        Quaternion rotation = Quaternion.Euler(0, rotationAmount * rotationSpeed * Time.deltaTime, 0);
        directionToCamera = rotation * directionToCamera;
        transform.position = pivotPoint.position + directionToCamera;

        transform.LookAt(pivotPoint);

        //store original rotation before look at
        Vector3 originalRotation = transform.eulerAngles;
        Vector3 originalPosition = transform.position;

        //transform.LookAt(pivotPoint);

        //revert rotation except y
        transform.position = originalPosition;
        transform.rotation = Quaternion.Euler(originalRotation.x, transform.eulerAngles.y, originalRotation.z);
        

        pivotOffset = transform.position - pivotPoint.position;
    }


}
