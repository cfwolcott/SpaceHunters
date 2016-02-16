using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class GameSettings
{
    // Size of play area
    public float PlayFieldSize_maxX = 50;
    public float PlayFieldSize_maxZ = 50;

    // Player settings
    public int NumberOfLives = 3;

    // Player's base location
    public Vector3 PlayerBasePosition;

    // Enemy base locatoin
    public Vector3 EnemyBasePosition;
}

public class GameController : MonoBehaviour 
{
    public GameSettings gGameSettings;  // reference to games setting class above

    // *** Game Objects ***
    // Camera
    private CameraController gMainCameraController; // This is the cameras reference to its CODE, not its OBJECT

    // Player
    public GameObject gPlayerObject;            // This is the player's ship object that was dragged in from the GUI
    private GameObject gPlayerObjectInstance;   // This is the object that will get passed around to all the enemy AI, camera, etc.

    // Asteroids
    public GameObject[] gAsteroidObjects;   // holds the different kinds of asteroid model objects (dragged in from the GUI). Not how many will actually be spawned
    public int gAsteroidCount = 10;
    //private AudioSource audioMusic;

    // Enemey
    public GameObject[] gEnemyObjects;  // holds the different kinds of enemy model objects. Not how many will actually be spawned
    public GameObject[] gListOfEnemies; // holds the number of enemy objects spawned in the game
    public int gEnemeyCount = 5;

    // UI Elements
    public Slider gHealthBarSlider;
    public Slider gCrystalLoadSlider;

    //-------------------------------------------------------------------------
    // Use this for initialization
	void Start () 
    {
        // Setup the camera object
        // This is the "dynamic" way to get an object. The static way would be to make the game object a public variable
        // and drag it into place in the Unity GUI. The Player, Enemy and Asteroid ojects are done staticly in the GUI.
        GameObject cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        if (cameraObject != null)
        {
            gMainCameraController = cameraObject.GetComponent<CameraController>();
        }

        // Setup the player and other game objects
        SpawnPlayer();
        SpawnAsteroids();
        SpawnEnemey(gPlayerObjectInstance);

        //audioMusic = GetComponent<AudioSource>();
        //audioMusic.loop = true;
        //AudioSource.PlayClipAtPoint(audioMusic.clip, transform.position);
	}

    //-------------------------------------------------------------------------
    public void ResetGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }

    //-------------------------------------------------------------------------
    public void QuitGame()
    {
        Application.Quit();
    }

    //-------------------------------------------------------------------------
    public void UI_SetSheildLevelMax(float value)
    {
        gHealthBarSlider.maxValue = value;
    }

    //-------------------------------------------------------------------------
    public void UI_SetSheildLevel( float value )
    {
        gHealthBarSlider.value = value;
    }

    //-------------------------------------------------------------------------
    public void UI_SetCargoLevelMax(float value)
    {
        gCrystalLoadSlider.maxValue = value;
    }

    //-------------------------------------------------------------------------
    public void UI_SetCargoLevel(float value)
    {
        gCrystalLoadSlider.value = value;
    }

    //-------------------------------------------------------------------------
    public void PlayerDead()
    {
        // Respawn a new player
        gGameSettings.NumberOfLives--;

        if (gGameSettings.NumberOfLives > 0)
        {
            Invoke("SpawnPlayer", 3);
        }
        else
        {
            // Game over!
        }
    }

    //-------------------------------------------------------------------------
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetGame();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            QuitGame();
        }

        // Update UI stuff
        gHealthBarSlider.value = Mathf.MoveTowards(gHealthBarSlider.value, 100.0f, 0.01f);
        gCrystalLoadSlider.value = Mathf.MoveTowards(gCrystalLoadSlider.value, 100.0f, 0.01f);
	}

    //-------------------------------------------------------------------------
    void SpawnPlayer()
    {
        Quaternion spawnRotation = Quaternion.identity;

        // Create the Player object
        gPlayerObjectInstance = (GameObject)Instantiate(gPlayerObject, gGameSettings.PlayerBasePosition, spawnRotation);

        // Set the camera to follow the player
        gMainCameraController.SetCameraPosition(gPlayerObjectInstance);
    }
   
    //-------------------------------------------------------------------------
    void SpawnEnemey( GameObject target )
    {
        enemyAI enemyAi;

        for (int i = 0; i < gEnemeyCount; i++)
		{
            // Randomly pick one of the 3 different asteroid objects we have to choose from
            GameObject enemeyObjectToSpawn = gEnemyObjects[Random.Range(0, gEnemyObjects.Length)];

            // Randomly place it in to our play field
            float maxX = gGameSettings.PlayFieldSize_maxX - 10;
            float maxZ = gGameSettings.PlayFieldSize_maxZ - 10;

            Vector3 spawnPosition = new Vector3(Random.Range(-maxX, maxX), 0, Random.Range(-maxZ, maxZ));
			
            Quaternion spawnRotation = Quaternion.identity;

            // Create the enemy object
            GameObject enemyInstance = (GameObject)Instantiate(enemeyObjectToSpawn, spawnPosition, spawnRotation);
            enemyAi = enemyInstance.GetComponent<enemyAI>();
            enemyAi.SetTarget(target);
		}
    }

    //-------------------------------------------------------------------------
    void SpawnAsteroids()
    {
        for (int i = 0; i < gAsteroidCount; i++)
		{
            // Randomly pick one of the 3 different asteroid objects we have to choose from
            GameObject asteroidObject = gAsteroidObjects[Random.Range(0, gAsteroidObjects.Length)];

            // Randomly scale for different size asteroids
            float scale = Random.Range(1, 3);
            asteroidObject.GetComponent<Transform>().localScale = new Vector3(scale, scale, scale);

            // Randomly place it in to our play field
            float maxX = gGameSettings.PlayFieldSize_maxX - 20;
            float maxZ = gGameSettings.PlayFieldSize_maxZ - 20;
            Vector3 spawnPosition = new Vector3(Random.Range(-maxX, maxX), 0, Random.Range(-maxZ, maxZ));
			
            Quaternion spawnRotation = Quaternion.identity;

            // Create the asteroid object
            Instantiate(asteroidObject, spawnPosition, spawnRotation);
		}
    }
}
