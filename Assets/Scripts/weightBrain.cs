using UnityEngine;
using System.Collections;

public class weightBrain : MonoBehaviour {
	
	// Update is called once per frame
	void FixedUpdate () {
        GetComponent<Rigidbody2D>().rotation = transform.parent.GetComponent<Rigidbody2D>().rotation;
	}
}
