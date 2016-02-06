using UnityEngine;
using System.Collections;

[System.Serializable]
public class GameSettings
{
    public float PlayFieldSize_maxX;
    public float PlayFieldSize_maxZ;
}

public class GameController : MonoBehaviour 
{
    public GameSettings gameSettings;
    public GameObject[] asteroidObjects;
    public int asteroidCount = 10;

	// Use this for initialization
	void Start () 
    {
        SpawnAsteroids();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Application.Quit();
        }
	}

    void SpawnAsteroids()
    {
        for (int i = 0; i < asteroidCount; i++)
		{
            // Randomly pick one of the 3 different asteroid objects we have to choose from
            GameObject asteroidObject = asteroidObjects[Random.Range(0, asteroidObjects.Length)];

            // Randomly scale for different size asteroids
            float scale = Random.Range(1, 3);
            asteroidObject.GetComponent<Transform>().localScale = new Vector3(scale, scale, scale);

            // Randomly place it in to our play field
            float maxX = gameSettings.PlayFieldSize_maxX - 20;
            float maxZ = gameSettings.PlayFieldSize_maxZ - 20;
            Vector3 spawnPosition = new Vector3(Random.Range(-maxX, maxX), 0, Random.Range(-maxZ, maxZ));
			
            Quaternion spawnRotation = Quaternion.identity;

            // Create the asteroid object
            Instantiate(asteroidObject, spawnPosition, spawnRotation);
		}
    }
}
