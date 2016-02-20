using UnityEngine;
using System.Collections;

public class enemyAI : MonoBehaviour 
{
    public Transform target; 
    public float moveSpeed;
    public float rotationSpeed;
    public float maxDetectRange = 20;
    public float maxWeaponRange = 15;

    // Weapon parameters
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;
	public string ourWeaponTag;     // so our own shots don't destory us

	public GameObject explosion;
	public float hitsToDestroy;


    private Transform myTransform;

    //-------------------------------------------------------------------------
    void Awake()
    {
        myTransform = transform;
    }

    //-------------------------------------------------------------------------
    void Start()
    {
		
    }

    //-------------------------------------------------------------------------
    public void SetTarget(GameObject newTarget)
    {
        target = newTarget.transform;
    }

    //-------------------------------------------------------------------------
    void FixedUpdate()
    {
        if (target != null)
        {
            Debug.DrawLine(target.position, myTransform.position, Color.red);

            // Detect and Follow logic
            float distanceToTarget = Vector3.Distance(target.position, myTransform.position);

			if (distanceToTarget < maxDetectRange && distanceToTarget > 4) 
			{
				// look at and Move towards target
				myTransform.rotation = Quaternion.Slerp (myTransform.rotation, Quaternion.LookRotation (target.position - myTransform.position), rotationSpeed * Time.deltaTime);
				myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
			}
			else 
			{
				// Wander around looking for player to kill
				myTransform.position += myTransform.forward * (moveSpeed / 2.0f) * Time.deltaTime;
			}

            // Weapon fire logic
            if (distanceToTarget <= maxWeaponRange)
            {
                FireWeapon();
            }
        }
    }

    //-------------------------------------------------------------------------
    void FireWeapon()
    {
        if (Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //GetComponent<AudioSource>().Play();
            AudioSource.PlayClipAtPoint(GetComponent<AudioSource>().clip, shotSpawn.position);
        }
    }

	//-------------------------------------------------------------------------
	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("AI: OnTriggerEnter called on tag: " + other.tag);

		if (other.tag == "Bolt" && other.tag != ourWeaponTag)
		{
			Destroy(other.gameObject);
			hitsToDestroy--;

			if (hitsToDestroy == 0)
			{
				if (explosion != null)
				{
					Instantiate(explosion, transform.position, transform.rotation);
				}

				Destroy(gameObject);
			}
		}
	}
}
