using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shipComputerPanel : MonoBehaviour
{

    public GameObject computer;
    public Vector3 myWorldPos;
    public Vector3 myWorldPosPlusHeight;
    public GameObject taggedDropdown;
    public GameObject[] computerTaggedObjects;
    public int maxTagAmount;
    LineRenderer line1;
    LineRenderer line2;

    void Start()
    {
        //maxTagAmount = computer.GetComponent<shipComputer>().maxTaggedObjects;
        //computerTaggedObjects = computer.GetComponent<shipComputer>().taggedObjects;
        transform.GetChild(0).GetComponent<Text>().text = (computer.GetComponent<shipComputer>().computerName);
        taggedDropdown = transform.GetChild(5).gameObject;
        line1 = transform.GetChild(2).GetComponent<LineRenderer>();
        line2 = transform.GetChild(3).GetComponent<LineRenderer>();
        for (int i=0;i<maxTagAmount;i++)
        {
            
        }
        taggedDropdown.GetComponent<Dropdown>().options = new System.Collections.Generic.List<Dropdown.OptionData>( );
    }

    void Update()
    {

        myWorldPos = Camera.main.ScreenToWorldPoint(transform.position);
        myWorldPos = new Vector3(myWorldPos.x, myWorldPos.y, 0f);
        myWorldPosPlusHeight = Camera.main.ScreenToWorldPoint(transform.position + Vector3.down * GetComponent<RectTransform>().sizeDelta.y);
        myWorldPosPlusHeight = new Vector3(myWorldPosPlusHeight.x, myWorldPosPlusHeight.y, 0f);
        line1.SetPosition(0, myWorldPos);
        line1.SetPosition(1, computer.transform.position);
        line2.SetPosition(0, myWorldPosPlusHeight);
        line2.SetPosition(1, computer.transform.position);
        line1.material = new Material(Shader.Find("Particles/Additive"));
        line2.material = new Material(Shader.Find("Particles/Additive"));
        line1.SetWidth(Camera.main.GetComponent<Camera>().orthographicSize * 0.01f, Camera.main.GetComponent<Camera>().orthographicSize * 0.01f);
        line2.SetWidth(Camera.main.GetComponent<Camera>().orthographicSize * 0.01f, Camera.main.GetComponent<Camera>().orthographicSize * 0.01f);

    }

    public void TaggingButton()
    {
        computer.GetComponent<shipComputer>().tagging = true;
    }

    public void OldTagClicked()
    {
        //computer.GetComponent<shipComputer>().powered = !computer.GetComponent<shipComputer>().powered;
        //poweredToggle.isOn = rocket.GetComponent<RocketBrain>().powered;
        //Debug.Log("Toggled!");
    }
}