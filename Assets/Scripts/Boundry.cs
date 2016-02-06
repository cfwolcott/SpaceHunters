using UnityEngine;
using System.Collections;

public class Boundry : MonoBehaviour {

    private Vector3 reverseDirection;
    public int speedReflectionVector = 1000;

    void OnTriggerExit(Collider other)
    {
        Debug.Log("Boundry OnTriggerExit");
        if (other.tag != "Bolt")
        {
            // reverse the x and z direction of travel to keep in bounds
            reverseDirection = Vector3.Reflect(other.GetComponent<Rigidbody>().velocity, Vector3.back );
            other.GetComponent<Rigidbody>().velocity = (reverseDirection.normalized * speedReflectionVector);
        }
    }

    void OnCollisionEnter( Collision collision )
    {
        Debug.Log("Boundry onCollisionEnter");
    }

    //void OnCollisionStay(Collision collision)
    //{
    //    Debug.Log("Boundry onCollisionStay");
    //}

    void OnCollisionExit(Collision collision)
    {
        Debug.Log("Boundry onCollisionExit");
    }
}
