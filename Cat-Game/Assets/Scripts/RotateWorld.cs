using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RotateWorld : MonoBehaviour
{
    public Transform pivotPoint;
    public float rotationAngle = 90f;
    public float duration = 2f;
    private Vector3 pivotOffset;
    private bool isRotating = false;

    //Checks if bathroom is blocking view only applicable for level 1
    public bool bathRoomCorner = false;

    private void Awake()
    {
        if (pivotPoint == null)
        {
            Debug.LogError("No pivot point assigned!");
            return;
        }

        pivotOffset = transform.position - pivotPoint.position;
    }

    private void Update()
    {
        if (!isRotating)
        {
            if (Keyboard.current.kKey.wasPressedThisFrame)
            {
                StartCoroutine(RotateCameraSmooth(-rotationAngle));
            }
            else if (Keyboard.current.lKey.wasPressedThisFrame)
            {
                StartCoroutine(RotateCameraSmooth(rotationAngle));
            }
        }
    }

    IEnumerator RotateCameraSmooth(float angle)
    {
        isRotating = true;

        Vector3 initialPosition = transform.position;
        Quaternion initialRotation = transform.rotation;

        Vector3 finalPosition = pivotPoint.position + (Quaternion.Euler(0, angle, 0) * pivotOffset);
        Quaternion finalRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + angle, transform.eulerAngles.z);

        float elapsedTime = 0;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(initialPosition, finalPosition, elapsedTime / duration);
            transform.rotation = Quaternion.Lerp(initialRotation, finalRotation, elapsedTime / duration);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Snap to the final position and rotation
        RotateCamera(angle);

        isRotating = false;
    }

    private void RotateCamera(float angle)
    {
        transform.RotateAround(pivotPoint.position, Vector3.up, angle);
        transform.position = pivotPoint.position + (Quaternion.Euler(0, angle, 0) * pivotOffset);

        if (transform.position.z > 14.0  && transform.position.x > 14.0) //bathroom corner
        {
            bathRoomCorner = true;
        }
        else bathRoomCorner = false;

        // Ensure the camera's forward direction is always looking at the pivot after rotation
        transform.LookAt(pivotPoint);
        Debug.Log(transform.position.z);
        Debug.Log(transform.position.x);
        pivotOffset = transform.position - pivotPoint.position;
        Debug.Log(bathRoomCorner);
    }
}