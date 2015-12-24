using UnityEngine;
using System.Collections;

public class mineBrain : MonoBehaviour {

    public float explosionSize;
    public float explosionPower;
    public float instantDetSpeed;
    public float explosionDelay;
    public float sensitivity;
    GameObject distanceDraw;
    float timer;
    public bool tripped;
    public bool huge;
    public bool inverse;
	// Use this for initialization

    void Awake()
    {
        distanceDraw = GameObject.Find("DistanceDrawing");
    }

    void Update ()
    {
        if (tripped)
        {
            timer++;
            if (timer>explosionDelay)
            {
                Explode();
            }
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.relativeVelocity.magnitude > instantDetSpeed) {
            Explode();
        }
        else if (other.relativeVelocity.magnitude > sensitivity)
        {
            tripped = true;
        }
    }

    void Explode()
    {
        if (huge)
        {
            GameObject bang = Instantiate(Resources.Load<GameObject>("HugeExplosion"));
            bang.GetComponent<CircleCollider2D>().radius = explosionSize;
            bang.GetComponent<explosion>().power = explosionPower;
            bang.GetComponent<explosion>().duration = 60;
            bang.transform.position = transform.position;
            distanceDraw.GetComponent<DistanceDrawing>().tempCircle.Add(bang);
            bang.transform.parent = distanceDraw.transform;
        }
        else if (inverse)
        {
            GameObject bang = Instantiate(Resources.Load<GameObject>("Implosion"));
            bang.GetComponent<CircleCollider2D>().radius = explosionSize;
            bang.GetComponent<implosion>().power = explosionPower;
            bang.GetComponent<implosion>().duration = 180;
            bang.transform.position = transform.position;
            distanceDraw.GetComponent<DistanceDrawing>().tempCircle.Add(bang);
            bang.transform.parent = distanceDraw.transform;
        }
        {
            GameObject bang = Instantiate(Resources.Load<GameObject>("Explosion"));
            bang.GetComponent<CircleCollider2D>().radius = explosionSize;
            bang.GetComponent<explosion>().power = explosionPower;
            bang.transform.position = transform.position;
            bang.GetComponent<explosion>().duration = 3;
            distanceDraw.GetComponent<DistanceDrawing>().tempCircle.Add(bang);
            bang.transform.parent = distanceDraw.transform;
        }
        Destroy(gameObject);
    }
}
