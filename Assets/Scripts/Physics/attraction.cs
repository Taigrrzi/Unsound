using UnityEngine;
using System.Collections;

public class attraction : MonoBehaviour
{

    public float power;

    void OnTriggerStay2D(Collider2D other)
    {
        float ratio = Vector3.Distance(transform.position, other.transform.position) / GetComponent<CircleCollider2D>().radius;
        ratio = Mathf.Clamp(ratio, 0, 1);
        if (other.gameObject.GetComponent<Rigidbody2D>() != null)
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce((transform.position- other.transform.position) * power * Mathf.Pow(1 - ratio, 2), ForceMode2D.Impulse);
        }
        else if (other.transform.GetComponentInParent<Rigidbody2D>() != null)
        {
            other.gameObject.GetComponentInParent<Rigidbody2D>().AddForce((transform.position - other.transform.position) * power * Mathf.Pow(1 - ratio, 2), ForceMode2D.Impulse);
        }
    }

}