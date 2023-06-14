using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform targetObject;

    public Vector3 cameraOffset;

    public float smoothFactor = 0.5f;

    public bool lookAtTarget = false;
    void Start()
    {
        cameraOffset = transform.position - targetObject.transform.position;
    }

    void LateUpdate()
    {
        Vector3 newPosition = cameraOffset + targetObject.transform.position;
        transform.position = Vector3.Slerp(transform.position, newPosition, smoothFactor);

        if(lookAtTarget )
        {
            transform.LookAt(targetObject);
        }
    }
}
