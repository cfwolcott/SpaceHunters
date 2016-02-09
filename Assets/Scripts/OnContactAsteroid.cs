using UnityEngine;
using System.Collections;

public class OnContactAsteroid : MonoBehaviour 
{
    public float hitsToDestroy;
    public GameObject explosion;

    //-------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("OnContactAsteroid OnTriggerEnter");

        // If its a "bolt" that hit us
        if (other.tag == "Bolt" || other.tag == "BoltEnemy")
        {
            Destroy(other.gameObject);  // Destroy only bolts that hit us
            
            // hitting asteroids with bolts will destory them eventually.
            // Player need to use their mining laser so asteroids are not destroyed!
            hitsToDestroy--;
        }

        // For now, destroy the asteroid. For SpaceHunters though, we're going to generate crystals!
        // After a certain amount of hits, then we destroy ourselves

        if (0 == hitsToDestroy)
        {
            // Destroy ourselves
            Destroy(gameObject);

            // Show the explosion animation
            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }
        }
        else
        {
            // TODO: play a sound for hitting the asteroid

            // TODO: show some animation for hitting the asteroid
        }
    }
}
