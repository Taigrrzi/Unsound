﻿using UnityEngine;
using System.Collections;

public class RocketBrain : MonoBehaviour {

    public bool active;
    public float thrustPower;
    public float myRotation;
    public float xComponent;
    public float yComponent;
    public float mass;
    public int maxKeys;
    public int[] boundKeys ;
    public bool pressed;
    public shipComputer shipcomputer;
    public int usedKeys = 0;

    void Start()
    {
        pressed = false;
        myRotation = transform.rotation.eulerAngles.z;
        transform.parent.GetComponent<Rigidbody2D>().mass += mass;
        xComponent = Mathf.Sin(Mathf.Deg2Rad * myRotation);
        yComponent = Mathf.Cos(Mathf.Deg2Rad * myRotation);
        if (boundKeys.Length == 0)
        {
            boundKeys = new int[maxKeys];
            for (int i = 0; i < maxKeys; i++)
            {
                boundKeys[i] = -1;
            }
        }
        else
        {
            int[] tempArray = boundKeys;
            usedKeys = tempArray.Length;
            boundKeys = new int[maxKeys];
            for (int i = 0; i < maxKeys; i++)
            {
                if (i<usedKeys)
                {
                    boundKeys[i] = tempArray[i];
                } else
                {
                    boundKeys[i] = -1;
                }
            }
        }
    }

    void FixedUpdate() {
        pressed = false;
        for (int i = 0; i < usedKeys; i++)
        {
            if (shipcomputer.keyBindingStates[boundKeys[i]]) {
            pressed = true;
            active = true;
            }
        }    
        
        if (pressed==false)
        {
            active = false;
        }
        GetComponent<Animator>().SetBool("RocketActive",active);
        if (active)
        {
            transform.parent.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(thrustPower*(-xComponent),thrustPower*yComponent),ForceMode2D.Force);
            transform.parent.GetComponent<Rigidbody2D>().AddTorque(((thrustPower*xComponent*transform.localPosition.y) + (thrustPower * yComponent * transform.localPosition.x)));
        }
	}
    /*
    void mapNaturalKeys()
    {
        if (yComponent == 1)
        {
           setNextBoundKey("Forward");
        } else if (yComponent == -1)
        {
            setNextBoundKey("Backward");
        }
        if (xComponent == 1)
        {
            setNextBoundKey("Left");
        }
        else if (xComponent == -1)
        {
            setNextBoundKey("Right");
        }
        if (thrustPower * xComponent * transform.localPosition.y>0)
        {
            setNextBoundKey("turnRight");
        } else if (thrustPower * xComponent * transform.localPosition.y < 0)
        {
            setNextBoundKey("turnLeft");
        }
    }
    */
    void setNextBoundKey(int Key)
    {
        for (int i=0;i<boundKeys.Length;i++)
        {
            if (boundKeys[i] == -1) {
                boundKeys[i] = Key;
            }
        }
    }
}
