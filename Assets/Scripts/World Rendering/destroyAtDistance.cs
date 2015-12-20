using UnityEngine;
using System.Collections;

public class destroyAtDistance : MonoBehaviour {
    public GameObject target;
    public float distance;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("PlayerShip");
	}
	
	// Update is called once per frame
	void Update () {
	    if (Vector3.Distance(transform.position,target.transform.position)<distance)
        {
            Destroy(gameObject);
        }
	}
}
