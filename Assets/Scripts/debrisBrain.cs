using UnityEngine;
using System.Collections;

public class debrisBrain : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody2D>().angularVelocity = Random.value*100;
        GetComponent<Rigidbody2D>().velocity = Vector2.right*((Random.value*2)-1) ;
    }
}
