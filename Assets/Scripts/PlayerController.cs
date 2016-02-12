using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour 
{
    // Our forward speed. Acceleration I guess.
    public float speed;
    // Our rotate speed.
    public float rotationSpeed;

    // Weapons
    public string ourWeaponTag;
    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;
    
    // Engines
    public GameObject engines;
    public GameObject smokeTrail;
    // The ships smoke trail particle emitter
    private ParticleSystem pe;

    // Ship life parameters
    public GameObject explosion;
    public float hitsToDestroy;

    // Pickups
    public int maxCrystalLoadCount;
    private int crystalLoadCount = 0;

    // UI Elements
    public Slider healthBarSlider;
    public Slider crystalLoadSlider;

    private Rigidbody rigidBody;
    private float speedModifier;

    //-------------------------------------------------------------------------
    // Use this for initialization
    void Start () 
    {
        rigidBody = GetComponent<Rigidbody>();
        pe = smokeTrail.GetComponent<ParticleSystem>();

        healthBarSlider.maxValue = hitsToDestroy;
        healthBarSlider.value = hitsToDestroy;

        crystalLoadSlider.value = 0;
        crystalLoadSlider.maxValue = maxCrystalLoadCount;
    }

    //-------------------------------------------------------------------------
    void Update()
    {
        // Shoot a bolt
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }

        // Shoot out the mining laser
        if (Input.GetButton("Fire2"))
        {
            Debug.Log("Fire2");
        }

        // Update health bar
        healthBarSlider.value = Mathf.MoveTowards(healthBarSlider.value, 100.0f, 0.01f);
        crystalLoadSlider.value = Mathf.MoveTowards(crystalLoadSlider.value, 100.0f, 0.01f);
    }

    //-------------------------------------------------------------------------
	void FixedUpdate () 
    {
        float yaw = Input.GetAxisRaw("Horizontal");
        float thrust = Input.GetAxisRaw("Vertical");

        // Engine Thrust
        if (thrust > 0)
        {
            engines.SetActive(true);

            pe.maxParticles = 50;
            speedModifier = 1.0f;
        }
        else
        {
            engines.SetActive(false);

            if (pe.maxParticles > 0)
            {
                pe.maxParticles -= 1;
            }

            speedModifier = 0.25f;
        }

        // Apply turning as a rotation.
        transform.Rotate(0, yaw * Time.deltaTime * rotationSpeed, rigidBody.velocity.y);

        // Apply thrust as a force.
        Vector3 force = transform.TransformDirection(0, 0, thrust * speed * speedModifier);
        rigidBody.AddForce(force);
	}

    //-------------------------------------------------------------------------
    // This function does the same as the "DestroyByContact" but is less generic. It is needed here
    // so the health bar can be updated
    void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "Bolt")
        //    Debug.Log("OnTriggerEnter: Bolt");
        //if (other.tag == "BoltEnemy")
        //    Debug.Log("OnTriggerEnter: BoltEnemy");

        if ((other.tag == "Bolt" || other.tag == "BoltEnemy") && other.tag != ourWeaponTag)
        {
            Destroy(other.gameObject);
            hitsToDestroy--;

            healthBarSlider.value = hitsToDestroy;

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

    //-------------------------------------------------------------------------
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Crystal")
        {
            if (crystalLoadCount < maxCrystalLoadCount)
            {
                crystalLoadCount++;
                crystalLoadSlider.value++;
                Destroy(collision.gameObject);
                //crystalPickupSound.Play();
            }
        }
    }
}
