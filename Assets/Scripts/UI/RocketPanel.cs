using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RocketPanel : MonoBehaviour
{

    public GameObject rocket;
    public Toggle poweredToggle;
    public Toggle forceOnToggle;
    public Vector3 myWorldPos;
    public Vector3 myWorldPosPlusHeight;

    void Start ()
    {
        poweredToggle = transform.GetChild(1).GetComponent<Toggle>();
        forceOnToggle = transform.GetChild(2).GetComponent<Toggle>();
        //BBB forceOnToggle.isOn = rocket.GetComponent<RocketBrain>().forceOn;
        //Debug.Log(rocket.GetComponent<RocketBrain>().forceOn);
        //Debug.Log(forceOnToggle.isOn);
        //BBB poweredToggle.isOn = rocket.GetComponent<RocketBrain>().powered;
        //BBB transform.GetChild(0).GetComponent<Text>().text = (rocket.GetComponent<RocketBrain>().rocketName);
        //BBB transform.GetChild(6).GetComponent<Text>().text = "Power: " + (rocket.GetComponent<RocketBrain>().thrustPower * 100);
    }

    void Update()
    {
        // Vector3 worldPoint = Camera.main.ScreenToWorldPoint(transform.position);
        LineRenderer line1 = transform.GetChild(4).GetComponent<LineRenderer>();
        LineRenderer line2 = transform.GetChild(5).GetComponent<LineRenderer>();

        myWorldPos = Camera.main.ScreenToWorldPoint(transform.position);
        myWorldPos = new Vector3(myWorldPos.x, myWorldPos.y, 0f);
        myWorldPosPlusHeight = Camera.main.ScreenToWorldPoint(transform.position + Vector3.down * GetComponent<RectTransform>().sizeDelta.y);
        myWorldPosPlusHeight = new Vector3(myWorldPosPlusHeight.x, myWorldPosPlusHeight.y, 0f);
        //line1.SetPosition(0, Camera.main.ScreenToWorldPoint(transform.position));
        line1.SetPosition(0, myWorldPos);
        line1.SetPosition(1, rocket.transform.GetChild(0).position);
        line2.SetPosition(0,myWorldPosPlusHeight);
        line2.SetPosition(1, rocket.transform.GetChild(0).position);
        line1.material = new Material(Shader.Find("Particles/Additive"));
        line2.material = new Material(Shader.Find("Particles/Additive")) ;
        line1.SetWidth(Camera.main.GetComponent<Camera>().orthographicSize * 0.01f, Camera.main.GetComponent<Camera>().orthographicSize * 0.01f);
        line2.SetWidth(Camera.main.GetComponent<Camera>().orthographicSize * 0.01f, Camera.main.GetComponent<Camera>().orthographicSize * 0.01f);

    }


    public void PoweredToggled ()
    {
        //BBB rocket.GetComponent<RocketBrain>().powered = !rocket.GetComponent<RocketBrain>().powered;
        //BBB poweredToggle.isOn = rocket.GetComponent<RocketBrain>().powered;
        //Debug.Log("Toggled!");
    }

    public void ForceOnToggled()
    {
        //BBB rocket.GetComponent<RocketBrain>().forceOn = !rocket.GetComponent<RocketBrain>().forceOn;
        //BBB forceOnToggle.isOn = rocket.GetComponent<RocketBrain>().forceOn;
        //Debug.Log("Toggled!");
    }
}
