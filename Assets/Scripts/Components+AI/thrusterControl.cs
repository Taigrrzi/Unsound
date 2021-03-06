﻿    using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class thrusterControl : MonoBehaviour {

    public bool active = false;
    public bool functional = true;
    public bool powered = true;

    public List<KeyCode> boundKeys;

    public float burstThrust;
    public float gainRate;
    public float baseThrust;
    public float haltRate;
    public bool burstRocket;

    public float currentThrust;
    float xComponent;
    float yComponent;
    ParticleSystem particles;

    public float targetThrustFraction=1;
    public float targetThrust;


	// Use this for initialization
	void Start () {
        xComponent = Mathf.Sin(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
        yComponent = Mathf.Cos(Mathf.Deg2Rad * transform.rotation.eulerAngles.z);
        targetThrust = baseThrust * targetThrustFraction;
        particles = transform.GetChild(0).GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        powered = GetComponent<shipComponentControl>().powered;
        if (powered)
        {
            targetThrust = baseThrust * targetThrustFraction;
            if (active)
            {
                if (currentThrust / Time.timeScale < targetThrust / Time.timeScale)
                {
                    currentThrust += gainRate*Time.deltaTime;
                }
                else if (Mathf.Abs(currentThrust - targetThrust) < gainRate*Time.deltaTime)
                {
                    currentThrust = targetThrust;
                }
                else
                {
                    currentThrust -= (currentThrust - targetThrust) / (10*Time.deltaTime);
                }
            }
            else
            {
                currentThrust -= haltRate*Time.deltaTime;
            }
            foreach (KeyCode kcode in boundKeys) {
                if (Input.GetKeyDown(kcode))
                {
                    //Debug.Log("ROCKET ON");
                    Activate();
                }
            }

            if (boundKeys.TrueForAll(new System.Predicate<KeyCode>((KeyCode k) => { if (Input.GetKey(k)) { return false; } else { return true; } })))
            {
                Deactivate();
            }
        } else
        {
            currentThrust -= haltRate;
        }
        if (currentThrust < 0)
        {
            currentThrust = 0;
        }
        particles.startSpeed = currentThrust / 10;
        particles.emissionRate = currentThrust * 10;
        particles.startSize = Mathf.Sqrt(currentThrust) / 30;
    }

    void FixedUpdate()
    {
        if (currentThrust > 0)
        {
            if (transform.parent != null)
            {
                transform.parent.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(currentThrust * (-xComponent), currentThrust * yComponent), ForceMode2D.Force);
                transform.parent.GetComponent<Rigidbody2D>().AddTorque(((currentThrust * xComponent * transform.localPosition.y) + (currentThrust * yComponent * transform.localPosition.x)));
            }
            else
            {
                GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(currentThrust * (-xComponent), currentThrust * yComponent), ForceMode2D.Force);
                //GetComponent<Rigidbody2D>().AddTorque(((currentThrust * xComponent * transform.localPosition.y) + (currentThrust * yComponent * transform.localPosition.x)));
            }
        }
        else
        {
            currentThrust = 0;
        }
        particles.startSpeed = currentThrust / 10;
        particles.emissionRate = currentThrust * 10;
        particles.startSize = Mathf.Sqrt(currentThrust) / 30; 
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
        particles.startSpeed = burstThrust / 10;
        particles.emissionRate = burstThrust * 10;
    }

    public void Deactivate()
    {
        active = false;
    }
}
