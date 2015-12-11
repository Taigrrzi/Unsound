using UnityEngine;
using System.Collections;

public class pingBrain : MonoBehaviour {

    //public GameObject followobject;
    int timer;
	
	// Update is called once per frame
	void Update () {
        //transform.position = followobject.transform.position;
        timer++;
        if (timer>(1/Time.deltaTime)*20)
        {
            Destroy(gameObject);
        }
	}
}
