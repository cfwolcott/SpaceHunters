using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    public float zoomScrollSpeed;

    private Vector3 offset;
    private float cameraDistanceMax = 20f;
    private float cameraDistanceMin = 5f;
    private float cameraDistance = 10f;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        cameraDistance -= Input.GetAxis("Mouse ScrollWheel") * zoomScrollSpeed;
        cameraDistance = Mathf.Clamp(cameraDistance, cameraDistanceMin, cameraDistanceMax);

        offset.y = cameraDistance;

        transform.position = player.transform.position + offset;
    }
}