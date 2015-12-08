using UnityEngine;
using System.Collections;

public class closeMyPanel : MonoBehaviour {

	public void Close (string type)
    { if (type == "Rocket")
        {
            transform.GetComponentInParent<RocketPanel>().rocket.GetComponent<RocketBrain>().panelOut = false;
            Destroy(transform.GetComponentInParent<dragPanel>().gameObject);
        } else if (type == "Ship")
        {
            transform.GetComponentInParent<shipComputerPanel>().computer.GetComponent<shipComputer>().panelOut = false;
            Destroy(transform.GetComponentInParent<dragPanel>().gameObject);
        } else if (type == "StationBase")
        {
            transform.GetComponentInParent<StationPanel>().station.GetComponent<spaceStationBrain>().basePanelOut = false;
            Destroy(transform.GetComponentInParent<dragPanel>().gameObject);
        }
    }
}
