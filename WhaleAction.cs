using UnityEngine;
using System.Collections;

public class WhaleAction : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void FixedUpdate()
    {
        transform.Translate(Vector3.forward * -0.02f);
	}
}
