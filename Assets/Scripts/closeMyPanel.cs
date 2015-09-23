using UnityEngine;
using System.Collections;

public class closeMyPanel : MonoBehaviour {

	public void Close ()
    {
        transform.GetComponentInParent<RocketPanel>().rocket.GetComponent<RocketBrain>().panelOut = false;
        Destroy(transform.GetComponentInParent<dragPanel>().gameObject);
    }
}
