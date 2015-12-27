using UnityEngine;
using System.Collections;

public class setOnClickAttach : MonoBehaviour {

	// Use this for initialization
	public void SetAttaching () {
        controllerBrain control = GameObject.Find("Controller").GetComponent<controllerBrain>();

        if (!(control.attaching))
        {
            control.attaching = true;

            control.A = transform.parent.gameObject;
            GameObject arrow = (GameObject)Instantiate(Resources.Load("Arrowhead"));
            arrow.GetComponent<drawArrow>().A = gameObject;
            arrow.transform.SetParent(FindObjectOfType<Canvas>().transform);
            control.currentArrow = arrow;
            control.connections.Add(arrow);
        }
    }
}
