using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float maxYAngle = 60f; // Maximum Y-axis angle

    private void LateUpdate()
    {
        // Assuming the camera is a child of Cinemachine Virtual Camera
        Transform vCamTransform = transform.parent;

        Vector3 lookDirection = player.position - vCamTransform.position;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

        float angleX = targetRotation.eulerAngles.x;

        if (angleX > maxYAngle && angleX < 180f)
        {
            angleX = maxYAngle;
        }
        else if (angleX > 180f && angleX < 360f - maxYAngle)
        {
            angleX = 360f - maxYAngle;
        }

        targetRotation = Quaternion.Euler(angleX, targetRotation.eulerAngles.y, targetRotation.eulerAngles.z);
        vCamTransform.rotation = targetRotation;
    }
}

