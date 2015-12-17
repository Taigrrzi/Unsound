using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timeSlider : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = GetComponent<Slider>().value;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
    }
}
