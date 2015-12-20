using UnityEngine;
using System.Collections;

public class triggerZone : MonoBehaviour {

    public GameObject mostRecentObject ;

    void OnTriggerEnter2D(Collider2D other)
    {
        mostRecentObject = other.gameObject;

        while (mostRecentObject.transform.parent != null)
        {
            mostRecentObject = mostRecentObject.transform.parent.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
       if ( mostRecentObject == other.gameObject)
        {
            mostRecentObject = null;
        }
    }

}
