using UnityEngine;
using System.Collections;

public class SensorDetector : MonoBehaviour 
{
	// Main game controller
	private enemyAI gAiCode;

	//-------------------------------------------------------------------------
	void Start () 
	{
		// Get the parent script so we can call its functions from here
		gAiCode = transform.parent.gameObject.GetComponent<enemyAI>();

		if (gAiCode != null) 
		{
			// Set our detection sphere's range
			GetComponent<SphereCollider> ().radius = gAiCode.maxDetectRange;
		}
	}
	
	//-------------------------------------------------------------------------
	void OnTriggerEnter(Collider other)
	{
		//Debug.Log("SensorDetector: OnTriggerEnter called on tag: " + other.tag);

		if (other.gameObject.tag == "Player")
		{
			if (gAiCode != null) 
			{
				Debug.Log ("Player Detected!");
				gAiCode.SetTarget (other.gameObject);
			}
		}
	}
}