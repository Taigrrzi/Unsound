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
        GetComponentInParent<shipComputer>().GetComponent<Rigidbody2D>().mass -= mass;
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
        if (other.relativeVelocity.magnitude>=detachVelocity)
        {
            Detach();
        }
    }
}
