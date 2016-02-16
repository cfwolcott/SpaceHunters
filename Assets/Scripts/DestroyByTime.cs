using UnityEngine;
using System.Collections;

public class DestroyByTime : MonoBehaviour
{
	public float lifetime;
    private AudioSource audioDestroySound;

    //-------------------------------------------------------------------------
	void Start ()
	{
        audioDestroySound = GetComponent<AudioSource>();

		//Destroy (gameObject, lifetime);
        //Invoke("DestroySelf", lifetime);
        StartCoroutine(DestroySelf( lifetime ));
	}

    //-------------------------------------------------------------------------
    IEnumerator DestroySelf( float lifeSpan )
    {
        yield return new WaitForSeconds(lifeSpan);

        // Play a "destroy" sound if available
        if (audioDestroySound != null)
        {
            AudioSource.PlayClipAtPoint(audioDestroySound.clip, gameObject.transform.position);
            yield return new WaitForSeconds(audioDestroySound.clip.length - 0.3f);
        }

        Destroy(gameObject);
    }
}
