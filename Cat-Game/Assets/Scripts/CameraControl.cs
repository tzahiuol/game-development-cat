using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;


public class CameraControl : MonoBehaviour
{
    public Transform pivotPoint;
    public Transform character;
    public float rotationSpeed = 20f;

    Vector3 offsetFromPivot;

    private void Start()
    {
        offsetFromPivot = transform.position - pivotPoint.position;
    }

    void Update()
    {
        float rotationAmount = 1f;
        var keyboard = Keyboard.current;

        // Check key presses for rotation.
        if (keyboard.oKey.isPressed)
        {
            // Rotate around the pivot point.
            RotateAroundPivot(-rotationAmount);
        }
        else if (keyboard.pKey.isPressed)
        {
            // Rotate around the pivot point.
            RotateAroundPivot(rotationAmount);
        }

        

        // Follow the character.
        Vector3 targetPosition = character.position + offsetFromPivot;
        transform.position = Vector3.Lerp(transform.position, targetPosition, 0.05f);
        transform.LookAt(character.position);
    }

    void RotateAroundPivot(float rotationAmount)
    {
        Vector3 directionToCamera = transform.position - pivotPoint.position;
        Quaternion rotation = Quaternion.Euler(0, rotationAmount * rotationSpeed * Time.deltaTime, 0);
        directionToCamera = rotation * directionToCamera;
        transform.position = pivotPoint.position + directionToCamera;
        offsetFromPivot = transform.position - pivotPoint.position;
    }
}





