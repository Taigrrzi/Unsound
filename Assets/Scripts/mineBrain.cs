using UnityEngine;
using System.Collections;

public class mineBrain : MonoBehaviour {

    public float explosionSize;
    public float explosionPower;
    public float explosionDelay;
    public float sensitivity;
    float timer;
    public bool tripped;
	// Use this for initialization

    void Update ()
    {
        if (tripped)
        {
            timer++;
            if (timer>explosionDelay)
            {
                GameObject bang = Instantiate(Resources.Load<GameObject>("Explosion"));
                bang.GetComponent<CircleCollider2D>().radius = explosionSize;
                bang.GetComponent<explosion>().power = explosionPower;
                bang.transform.position = transform.position;
                bang.GetComponent<explosion>().duration = 3;
                Destroy(gameObject);
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > sensitivity)
        {
            tripped = true;
        }
    }
}
