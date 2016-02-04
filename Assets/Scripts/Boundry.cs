using UnityEngine;
using System.Collections;

public class Boundry : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void onCollisionEnter( Collision collision )
    {
        Debug.Log("Boundry onCollisionEnter");
    }
}
