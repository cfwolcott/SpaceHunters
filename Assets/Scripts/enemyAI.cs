using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour 
{
    public Transform target; 
    public float moveSpeed;
    public float rotationSpeed;
    public float maxDistance = 5;
    public float maxDetectRange = 20;
    public float maxWeaponRange = 15;

    // Weapon parameters
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    private Transform myTransform;

    //-------------------------------------------------------------------------
    void Awake()
    {
        myTransform = transform;
    }

    //-------------------------------------------------------------------------
    void Start()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Player");
        target = go.transform;
    }

    //-------------------------------------------------------------------------
    void FixedUpdate()
    {
        Debug.DrawLine(target.position, myTransform.position, Color.red);

        // Detect and Follow logic
        float distanceToTarget = Vector3.Distance(target.position, myTransform.position);

        if (distanceToTarget > maxDistance && distanceToTarget < maxDetectRange)
        {
            // look at and Move towards target
            myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
            myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
        }

        // Weapon fire logic
        if (distanceToTarget <= maxWeaponRange)
        {
            FireWeapon();
        }
    }

    //-------------------------------------------------------------------------
    void FireWeapon()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }
    }
}
