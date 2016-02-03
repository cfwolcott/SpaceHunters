using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
    public float speed;

    private Rigidbody rigidBody;

    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
