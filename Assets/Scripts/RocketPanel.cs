using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RocketPanel : MonoBehaviour
{

    public GameObject rocket;
    public Toggle poweredToggle;
    public Toggle forceOnToggle;

    void Start ()
    {
        poweredToggle = transform.GetChild(1).GetComponent<Toggle>();
        forceOnToggle = transform.GetChild(2).GetComponent<Toggle>();
        forceOnToggle.isOn = rocket.GetComponent<RocketBrain>().forceOn;
        //Debug.Log(rocket.GetComponent<RocketBrain>().forceOn);
        //Debug.Log(forceOnToggle.isOn);
        poweredToggle.isOn = rocket.GetComponent<RocketBrain>().powered;
        transform.GetChild(0).GetComponent<Text>().text = (rocket.GetComponent<RocketBrain>().rocketName);
        transform.GetChild(6).GetComponent<Text>().text = "Power: " + (rocket.GetComponent<RocketBrain>().thrustPower * 100);
    }

    void Update()
    {
       // Vector3 worldPoint = Camera.main.ScreenToWorldPoint(transform.position);
        transform.GetChild(4).GetComponent<LineRenderer>().SetPosition(0, Camera.main.ScreenToWorldPoint(transform.position));
        transform.GetChild(4).GetComponent<LineRenderer>().SetPosition(1, rocket.transform.GetChild(0).position);
        transform.GetChild(5).GetComponent<LineRenderer>().SetPosition(0, Camera.main.ScreenToWorldPoint(transform.position+Vector3.down*GetComponent<RectTransform>().sizeDelta.y));
        transform.GetChild(5).GetComponent<LineRenderer>().SetPosition(1, rocket.transform.GetChild(0).position);
    }
	

    public void PoweredToggled ()
    {
        rocket.GetComponent<RocketBrain>().powered = !rocket.GetComponent<RocketBrain>().powered;
        poweredToggle.isOn = rocket.GetComponent<RocketBrain>().powered;
        //Debug.Log("Toggled!");
    }

    public void ForceOnToggled()
    {
        rocket.GetComponent<RocketBrain>().forceOn = !rocket.GetComponent<RocketBrain>().forceOn;
        forceOnToggle.isOn = rocket.GetComponent<RocketBrain>().forceOn;
        //Debug.Log("Toggled!");
    }
}
