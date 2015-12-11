using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {

    public float power;
    public float timer;
    public float duration = 24;

    void Update ()
    {
        timer++;
          if (timer>duration)
        {
            GetComponent<ParticleSystem>().startLifetime = GetComponent<CircleCollider2D>().radius/5;
            Destroy(gameObject);
        }
    }
	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) {
        float ratio = (GetComponent<CircleCollider2D>().radius/(GetComponent<CircleCollider2D>().radius+Vector3.Distance(transform.position, other.transform.position)));
        if (other.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForceAtPosition((other.transform.position - transform.position) * power * ratio, transform.position, ForceMode2D.Impulse);
        } else if (other.transform.GetComponentInParent<Rigidbody2D>()!= null) {
            other.gameObject.GetComponentInParent<Rigidbody2D>().AddForceAtPosition((other.transform.position - transform.position) * power * ratio, transform.position, ForceMode2D.Impulse);
        }
        // other.gameObject.GetComponent<Rigidbody2D>().AddTorque(((thrustPower * xComponent * transform.localPosition.y) + (thrustPower * yComponent * transform.localPosition.x)));
    }

}
