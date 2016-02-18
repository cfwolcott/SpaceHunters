using UnityEngine;
using System.Collections;

public class CrystalCollectorCore : MonoBehaviour 
{
    // Main game controller
    private GameController gGameController;

    //-------------------------------------------------------------------------
    void Start()
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
    }

    //-------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Crystal")
        {
            // Increment the number of crystals collected to the GameManager
            gGameController.UI_UpdateCrystalCounter();

            // destroy the crystal object
            Destroy(other.gameObject);
        }
    }
}
