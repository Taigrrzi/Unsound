using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class timeSlider : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
        Time.timeScale = GetComponent<Slider>().value;
        Time.fixedDeltaTime = 0.02F * Time.timeScale;
        transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = (Mathf.Round(Time.timeScale * 10) / 10) + "x";
    }
}
