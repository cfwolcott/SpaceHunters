﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    // Our forward speed. Acceleration I guess.
    public float speed;
    // Our rotate speed.
    public float rotationSpeed;

    public GameObject shot;
    public GameObject engines;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    private Rigidbody rigidBody;
    private float speedModifier;

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
            GetComponent<AudioSource>().Play();
        }
    }
	
	void FixedUpdate () 
    {
        float yaw = Input.GetAxisRaw("Horizontal");
        float thrust = Input.GetAxisRaw("Vertical");

        // Show engine thrust only when thrusting forward
        //Debug.Log("thrust: " + thrust);
        if (thrust > 0)
        {
            engines.SetActive(true);
            speedModifier = 1.0f;
        }
        else
        {
            engines.SetActive(false);
            speedModifier = 0.25f;
        }

        // Apply turning as a rotation.
        transform.Rotate(0, yaw * Time.deltaTime * rotationSpeed, rigidBody.velocity.y);

        // Apply thrust as a force.
        Vector3 force = transform.TransformDirection(0, 0, thrust * speed * speedModifier);
        rigidBody.AddForce(force);
	}
}
