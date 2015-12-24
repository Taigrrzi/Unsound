using UnityEngine;
using System.Collections;

public class shipComponentControl : MonoBehaviour {

    public float mass;
    public bool attached;
    public bool powered;
    public float detachVelocity;
    public GameObject bulwark;
    Rigidbody2D rbd;

	// Use this for initialization
	void Start () {
        if (bulwark!=null)
        {
            bulwark.GetComponent<shipBulwarkControl>().attachedComponents.Add(gameObject);
        }
        if (!attached)
        {
            rbd = gameObject.AddComponent<Rigidbody2D>();
            rbd.mass = mass;
            rbd.gravityScale = 0;
            rbd.angularDrag = 0;
        } else
        {
            GetComponentInParent<shipComputer>().GetComponent<Rigidbody2D>().mass += mass;
        }
	}

    public void Detach()
    {
        if (attached)
        {
            GetComponentInParent<Rigidbody2D>().mass -= mass;
            attached = false;
            transform.parent = null;
            rbd = gameObject.AddComponent<Rigidbody2D>();
            if (bulwark!=null)
            {
                bulwark.GetComponent<shipBulwarkControl>().attachedComponents.Remove(gameObject);
            }
            rbd.mass = mass;
            rbd.gravityScale = 0;
            rbd.angularDrag = 0;
            powered = false;
        } else
        {
            Debug.LogError("Trying to detach when detached: "+transform.position.x);
        }
    }

    public void Attach(GameObject ship)
    {
        attached = true;
        transform.parent = ship.transform;
        rbd.Sleep();
        ship.GetComponent<Rigidbody2D>().mass += mass;
        Destroy(GetComponent<Rigidbody2D>());
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude >= detachVelocity && detachVelocity != -1)
        {
            if (attached)
            {
                Detach();
            }
        }
    }
}
