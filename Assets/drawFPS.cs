using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class drawFPS : MonoBehaviour {

	// Update is called once per frame
	void Update () {
        GetComponent<Text>().text = "FPS: "+Mathf.FloorToInt(1f / Time.deltaTime);
	}
}
