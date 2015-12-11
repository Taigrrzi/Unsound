using UnityEngine;
using System.Collections;

public class conformToCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Camera mainCam = Camera.main;
        transform.localScale = new Vector3 (mainCam.orthographicSize * (16f / 9f) * 20, mainCam.orthographicSize * 20);
        transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, transform.position.z);
        transform.rotation = mainCam.transform.rotation;
	}
}
