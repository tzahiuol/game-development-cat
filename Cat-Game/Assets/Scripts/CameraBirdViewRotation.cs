using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBirdViewRotation : MonoBehaviour
{
    public Transform birdViewTransform;
    public Transform worldCenter;
    public float transitionSpeed = 1f;
    public float groundCircleRadius = 40f;

    [Header("Offsets")]
    // How much above the actual ground the camera will be stopping the orbit
    public float groundHeightOffset = 5f;

    private float lerpTime = 0.5f;
    private float groundAngle = 0f;

private void Update()
{
    //keyboard control for orbit between original camera angle view and bird view or ground level
    if (Input.GetKey(KeyCode.W) && lerpTime < 1)
        lerpTime += Time.deltaTime * transitionSpeed;
    else if (Input.GetKey(KeyCode.S) && lerpTime > 0)
        lerpTime -= Time.deltaTime * transitionSpeed;

    //keyboard control for rotation change which will affect the orbit 
    if (Input.GetKey(KeyCode.D))
        groundAngle += Time.deltaTime * transitionSpeed * 40f;
    else if (Input.GetKey(KeyCode.A))
        groundAngle -= Time.deltaTime * transitionSpeed * 40f;

    lerpTime = Mathf.Clamp(lerpTime, 0, 1);

    // Adjust the ground level position based on the offset
    Vector3 groundPosition = worldCenter.position + new Vector3(groundCircleRadius * Mathf.Cos(groundAngle * Mathf.Deg2Rad), groundHeightOffset, groundCircleRadius * Mathf.Sin(groundAngle * Mathf.Deg2Rad));

    transform.position = Vector3.Lerp(groundPosition, birdViewTransform.position, lerpTime);

    //camera to always look at World centre
    transform.LookAt(worldCenter);
}
}

