using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float zoomScrollSpeed;

    private GameObject cameraTarget;
    private Vector3 offset;
    private Vector3 gLastTargetPosition;
    private float cameraDistanceMax = 20f;
    private float cameraDistanceMin = 5f;
    private float cameraDistance = 10f;
    
    //-------------------------------------------------------------------------
    // Must call this function to set what game object you want the camera to look at (usually from GameController)
    public void SetCameraPosition(GameObject target)
    {
        cameraTarget = target;

        // Move camera to location of new target
        transform.position = cameraTarget.transform.position;

        offset = transform.position - cameraTarget.transform.position;
    }

    //-------------------------------------------------------------------------
    // Update is called once per frame
    void LateUpdate()
    {
        cameraDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomScrollSpeed;
        cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);

        offset.y = cameraDistance;

        if (cameraTarget != null)
        {
            gLastTargetPosition = cameraTarget.transform.position;
            transform.position = gLastTargetPosition + offset;   
        }
        else
        {
            transform.position = gLastTargetPosition + offset;
        }
    }
}