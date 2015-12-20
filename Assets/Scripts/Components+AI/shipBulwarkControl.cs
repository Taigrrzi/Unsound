using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class shipBulwarkControl : MonoBehaviour
{

    public float mass;
    public bool attached;
    public bool powered;
    public float detachVelocity;
    public GameObject[] componentSpots;
    public List<GameObject> attachedComponents;
    Rigidbody2D rbd;

    // Use this for initialization
    void Start()
    {
        if (!attached)
        {
            rbd = gameObject.AddComponent<Rigidbody2D>();
            rbd.mass = mass;
            rbd.gravityScale = 0;
            rbd.angularDrag = 0;
        } else
        {
            transform.parent.GetComponent<Rigidbody2D>().mass += mass;
        }
        foreach (GameObject comp in attachedComponents)
        {
            comp.GetComponent<shipComponentControl>().powered = powered;
        }
    }

    void Update()
    {
        foreach (GameObject comp in attachedComponents)
        {
            comp.GetComponent<shipComponentControl>().powered = powered;
        }
    }

    public void Detach()
    {
        powered = false;
        foreach (GameObject comp in attachedComponents)
        {
            comp.transform.parent = transform;
            comp.GetComponent<shipComponentControl>().powered = false;
            //comp.GetComponent<shipComponentControl>().Detach();
        }
        transform.parent.GetComponent<Rigidbody2D>().mass -= mass;
        attached = false;
        transform.parent = null;
        rbd = gameObject.AddComponent<Rigidbody2D>();
        rbd.mass = mass;
        rbd.gravityScale = 0;
        rbd.angularDrag = 0;
    }

    public void Attach(GameObject ship)
    {
        attached = true;
        transform.parent = ship.transform;
        rbd.Sleep();
        ship.GetComponent<Rigidbody2D>().mass += mass;
        Destroy(GetComponent<Rigidbody2D>());
    }

    void OnCollisionEntered2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude >= detachVelocity&&detachVelocity!=-1)
        {
            Detach();
        }
    }
}
