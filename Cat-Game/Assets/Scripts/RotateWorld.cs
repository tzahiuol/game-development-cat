using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class RotateWorld : MonoBehaviour
{
    public float rotationSpeed = 20f;

    // Update is called once per frame
    void Update()
    {
        float rotationAmount = 0f;

        // Check key presses for rotation.
        if (Keyboard.current.oKey.isPressed)
        {
            rotationAmount = -1f;
        }
        else if (Keyboard.current.pKey.isPressed)
        {
            rotationAmount = 1f;
        }

        // Rotate around the Y-axis
        transform.Rotate(0, rotationAmount * rotationSpeed * Time.deltaTime, 0);
    }
}