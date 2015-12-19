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
    float xComponent;
    float yComponent;
    public ParticleSystem particles;

    public float targetThrustFraction=1;
    public float targetThrust;


	// Use this for initialization
	void Start () {
        xComponent = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
        yComponent = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
        targetThrust = baseThrust * targetThrustFraction;
        transform.parent.GetComponent<Rigidbody2D>().mass += mass;
        particles = transform.GetChild(0).GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        targetThrust = baseThrust * targetThrustFraction;
        if (active)
        {
            if (currentThrust<targetThrust)
            {
                currentThrust += gainRate;
            } else if (Mathf.Abs(currentThrust-targetThrust)<gainRate)
            {
                currentThrust = targetThrust;
            } else
            {
                currentThrust -= (currentThrust - targetThrust) / 10;
            }
        } else
        {
            currentThrust -= haltRate;
        }

        if (currentThrust>0)
        {
            transform.parent.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(currentThrust * (-xComponent), currentThrust * yComponent), ForceMode2D.Force);
            transform.parent.GetComponent<Rigidbody2D>().AddTorque(((currentThrust * xComponent * transform.localPosition.y) + (currentThrust * yComponent * transform.localPosition.x)));
        } else
        {
            currentThrust = 0;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            //Debug.Log("ROCKET ON");
            Activate();
        } else if (Input.GetKeyUp(KeyCode.W))
        {
            Deactivate();
        }
        particles.startSpeed = currentThrust / 2;
    }

    public void Activate()
    {
        if (currentThrust ==0 )
        {
            currentThrust = burstThrust;
            //transform.parent.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(burstThrust * (-xComponent), burstThrust * yComponent), ForceMode2D.Impulse);
            //transform.parent.GetComponent<Rigidbody2D>().AddTorque(((currentThrust * burstThrust * transform.localPosition.y) + (burstThrust * yComponent * transform.localPosition.x)),ForceMode2D.Impulse);
        }
        active = true;
        particles.startSpeed = burstThrust / 2;
    }

    public void Deactivate()
    {
        active = false;
    }
}
