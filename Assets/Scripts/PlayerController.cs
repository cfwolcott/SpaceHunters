﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    // Our forward speed. Acceleration I guess.
    public float speed;
    // Our rotate speed.
    public float rotationSpeed;
    // Tilt the ship when it turns
    public float tilt;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    private Rigidbody rigidBody;

	// Use this for initialization
	void Start () 
    {
        rigidBody = GetComponent<Rigidbody>();
	}

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            //GameObject clone = 
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation); // as GameObject;
        }
    }
	
	void FixedUpdate () 
    {
        float yaw = Input.GetAxisRaw("Horizontal");
        float thrust = Input.GetAxisRaw("Vertical");

        // Apply turning as a rotation.
        transform.Rotate(0, yaw * Time.deltaTime * rotationSpeed, rigidBody.velocity.y * -tilt);

        // Apply thrust as a force.
        Vector3 force = transform.TransformDirection(0, 0, thrust * speed);
        rigidBody.AddForce(force);

        //rigidBody.rotation = Quaternion.Euler(0, 0, rigidBody.velocity.x * -tilt);
	}
}