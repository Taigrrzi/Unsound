using UnityEngine;
using System.Collections;

public class taggable : MonoBehaviour {
   /* public shipComputer computer ;
    public bool tagged = false;
	// Update is called once per frame
    void Start()
    {
        computer = GameObject.Find("PlayerShip").GetComponent<shipComputer>();
    }

	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            if (GetComponent<Collider2D>().OverlapPoint(Camera.main.ScreenToWorldPoint(Input.mousePosition)))
            {
                if (computer.tagging==true)
                {
                    if (!tagged)
                    {
                        computer.ta(gameObject);
                        tagged = true;
                    } else
                    {
                        tagged = false;
                    }
                    computer.tagging = false;
                }
            }
        }
    }

    void OnDestroy ()
    {
        computer.taggedObjects.Remove(gameObject);
    }*/
}
