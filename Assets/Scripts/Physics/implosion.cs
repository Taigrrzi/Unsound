using UnityEngine;
using System.Collections;

public class implosion : MonoBehaviour
{

    public float power;
    public float timer;
    public float duration = 24;

    void Update()
    {
        timer+=(Time.deltaTime*60);
        if (timer > duration)
        {
            GetComponent<ParticleSystem>().startLifetime = GetComponent<CircleCollider2D>().radius / 5;
            Destroy(gameObject);
        }
    }
    // Use this for initialization
    void OnTriggerStay2D(Collider2D other)
    {
        //float ratio = (GetComponent<CircleCollider2D>().radius / (GetComponent<CircleCollider2D>().radius + Vector3.Distance(transform.position, other.transform.position)));
        float ratio = Vector3.Distance(transform.position, other.transform.position) / GetComponent<CircleCollider2D>().radius;
        ratio = Mathf.Clamp(ratio,0,1);
        if (other.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce((transform.position - other.transform.position) * power* Mathf.Pow(1 - ratio, 2), ForceMode2D.Impulse);
        }
        else if (other.transform.GetComponentInParent<Rigidbody2D>() != null)
        { //* Mathf.Pow(1-ratio, 2)
            other.gameObject.GetComponentInParent<Rigidbody2D>().AddForce((transform.position - other.transform.position) * power * Mathf.Pow(1 - ratio, 2), ForceMode2D.Impulse);
        }
    }

}
