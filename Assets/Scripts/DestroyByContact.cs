using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour 
{
    public string ourWeaponTag;     // so our own shots don't destory us
    public GameObject explosion;
    public float hitsToDestroy;

    //-------------------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bolt" )
            Debug.Log("OnTriggerEnter: Bolt");
        if (other.tag == "BoltEnemy")
            Debug.Log("OnTriggerEnter: BoltEnemy");

        if ((other.tag == "Bolt" || other.tag == "BoltEnemy") && other.tag != ourWeaponTag)
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
