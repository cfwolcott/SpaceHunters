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

    // Cargo Pickups
    public int maxCargoLoadCount;
    private int cargoLoadCount = 0;
    public GameObject cargoObject;
    public Transform cargoDumpSpawn;

    //// UI Elements
    //public Slider healthBarSlider;
    //public Slider crystalLoadSlider;

    private Rigidbody rigidBody;
    private float speedModifier;

    // Sound clips
    private AudioSource audioWeapon;
    private AudioSource audioCrystalPickup;

    // Main game controller
    private GameController gGameController;


    //-------------------------------------------------------------------------
    // Use this for initialization
    void Start () 
    {
        GameObject gameControllerObject = GameObject.FindGameObjectWithTag("GameController");
        if (gameControllerObject != null)
        {
            gGameController = gameControllerObject.GetComponent<GameController>();
        }

        if (gGameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }

        rigidBody = GetComponent<Rigidbody>();
        pe = smokeTrail.GetComponent<ParticleSystem>();

        gGameController.UI_SetSheildLevelMax(hitsToDestroy);
        gGameController.UI_SetSheildLevel(hitsToDestroy);

        gGameController.UI_SetCargoLevelMax(maxCargoLoadCount);
        gGameController.UI_SetCargoLevel(0);

        // Assign audio sources
        AudioSource[] audioClips = GetComponents<AudioSource>();

        // Load clips. These are in order that they appear in the inspector
        audioWeapon = audioClips[0];
        audioCrystalPickup = audioClips[1];
    }

    //-------------------------------------------------------------------------
    void Update()
    {
        // Shoot a bolt
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            //GetComponent<AudioSource>().Play();
            audioWeapon.Play();
        }

        // Shoot out the mining laser
        if (Input.GetButton("Fire2"))
        {
            Debug.Log("Fire2");
        }

        // Defined ship key controls
        // C - Cargo, dump crystals that are in the cargo hold
        if (Input.GetKeyDown(KeyCode.C))
        {
            DumpCrystal(cargoDumpSpawn);
        }

        // Update health bar
        //gameController.healthBarSlider.value = Mathf.MoveTowards(healthBarSlider.value, 100.0f, 0.01f);
        //gameController.crystalLoadSlider.value = Mathf.MoveTowards(crystalLoadSlider.value, 100.0f, 0.01f);
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
            // Go slower in reverse
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
    // so the health bar can be updated and player objects can be respawned
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

            //gameController.healthBarSlider.value = hitsToDestroy;
            gGameController.UI_SetSheildLevel(hitsToDestroy);

            if (hitsToDestroy == 0)
            {
                if (explosion != null)
                {
                    Instantiate(explosion, transform.position, transform.rotation);
                }

                Destroy(gameObject);

                gGameController.PlayerDead();
            }
        }
    }

    //-------------------------------------------------------------------------
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Crystal")
        {
            if (cargoLoadCount < maxCargoLoadCount)
            {
                cargoLoadCount++;
                //gameController.crystalLoadSlider.value++;
                gGameController.UI_SetCargoLevel(cargoLoadCount);
                Destroy(collision.gameObject);
                audioCrystalPickup.Play();
            }
        }
    }

    //-------------------------------------------------------------------------
    void DumpCrystal(Transform spawnTransform)
    {
        if (cargoLoadCount > 0 && cargoObject != null)
        {
            cargoLoadCount--;
            //gameController.crystalLoadSlider.value--;
            gGameController.UI_SetCargoLevel(cargoLoadCount);
            GameObject xtal = (GameObject)Instantiate(cargoObject, spawnTransform.position, spawnTransform.rotation);
            xtal.GetComponent<Rigidbody>().velocity = transform.forward * -2.0f;

        }
    }
}
