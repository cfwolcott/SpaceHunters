using UnityEngine;
using System.Collections;

public class Mover : MonoBehaviour 
{
    public float speed;
    //public float lifeTime;

    private Rigidbody rigidBody;

    //-------------------------------------------------------------------------
    void Start()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed;

        //// Destroy the projectile in "life" seconds.
        //Invoke("Hit", lifeTime);
    }

    /**
    * The method called when the DynamicObject component registers a collsion.
    */
    //public void Hit()
    //{
    //    Destroy(gameObject);
    //}
}
