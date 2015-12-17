using UnityEngine;
using System.Collections;

public class rocketControl : MonoBehaviour {

    public bool active = false;
    public bool functional = true;
    public bool powered = true;

    public float burstThrust;
    public float gainRate;
    public float baseThrust;
    public float haltRate;
    public float mass;
    public bool burstRocket;

    public float currentThrust;
    public bool bursting;
    public float xComponent;
    public float yComponent;

    public float targetThrustFraction=1;
    public float targetThrust;


	// Use this for initialization
	void Start () {
        targetThrust = baseThrust * targetThrustFraction;
        transform.parent.GetComponent<Rigidbody2D>().mass += mass;
    }
	
	// Update is called once per frame
	void Update () {
	    if (active)
        {
            if (burstRocket)
            {
                if (bursting)
                {
                    if (currentThrust<burstThrust)
                    {

                    }
                }
            }
        }

        if (currentThrust>0)
        {
            xComponent = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
            yComponent = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
            transform.parent.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(currentThrust * (-xComponent), currentThrust * yComponent), ForceMode2D.Force);
            transform.parent.GetComponent<Rigidbody2D>().AddTorque(((currentThrust * xComponent * transform.localPosition.y) + (currentThrust * yComponent * transform.localPosition.x)));
        } else
        {
            currentThrust = 0;
        }
	}

    public void activate()
    {
        if (currentThrust ==0 )
        {
            xComponent = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
            yComponent = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
            transform.parent.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(burstThrust * (-xComponent), burstThrust * yComponent), ForceMode2D.Force);
            transform.parent.GetComponent<Rigidbody2D>().AddTorque(((currentThrust * burstThrust * transform.localPosition.y) + (burstThrust * yComponent * transform.localPosition.x)));
        }
        active = true; 
    }
}
