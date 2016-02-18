using UnityEngine;
using System.Collections;

// This is the outer "shell" of the gravity fed Crystal Collector object
// Players need to GENTLY release crystals into the collector so that they hit the core.
// When the core detects the crystal (CrystalCollectorCore.cs), it will absorb and count crystals collected to the GameManager
public class CrystalCollectorOuter : MonoBehaviour 
{
    public float gravityForce = 5;

    //-------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        //if (other.gameObject.tag == "Crystal")
        {
            Gravitate(other);
        }
    }

    //-------------------------------------------------------------------------
    void Gravitate(Collider obj)
    {
        Rigidbody objRigidBody = obj.GetComponent<Rigidbody>();
        Transform objTransform = obj.GetComponent<Transform>();

        transform.LookAt(objTransform);

        Vector3 dir = transform.position - objTransform.position;

        Debug.DrawLine(transform.position, objTransform.position, Color.red);

        float dist = Vector3.Distance(transform.position, objTransform.position);

        if (objRigidBody)
        {
            objRigidBody.AddForce((dir) * (gravityForce * dist));
        }
    }
}
