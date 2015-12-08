using UnityEngine;
using System.Collections;

public class efficientDrawing : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        EnableRecursive(transform);
        //Debug.Log("Drawing Efficiency");
	}

    void EnableRecursive(Transform group)
    {
        for(int i=0; i<group.childCount;i++)
        {
            Transform current = group.GetChild(i);
            if (current.tag=="Group")
            {
                EnableRecursive(current);
            } else
            {
                if (Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1)), current.position)<24)
                {
                    current.gameObject.SetActive(true);
                } else
                {
                    //Debug.Log("Deactivate");
                    current.gameObject.SetActive(false);
                }
            }
        }
    }
}
