using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class drawFPS : MonoBehaviour {

    int[] pastFps = new int[60];

    void Start()
    {
        for (int i=0; i<60;i++)
        {
            pastFps[i] = 60;
        }
    }

	// Update is called once per frame
	void Update () {
        pastFps[59] = Mathf.FloorToInt(1f / Time.deltaTime);
        int averageFps = 0;
        for (int i = 1; i < 60; i++)
        {
            pastFps[i-1] = pastFps[i];
            averageFps += pastFps[i];
        }
        GetComponent<Text>().text = "FPS: "+Mathf.RoundToInt(averageFps/60);
	}
}
