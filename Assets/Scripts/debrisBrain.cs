using UnityEngine;
using System.Collections;

public class debrisBrain : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //GetComponent<Rigidbody2D>().angularVelocity = 80;
        GetComponent<Rigidbody2D>().velocity = Vector2.right*0.5f ;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
