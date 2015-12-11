using UnityEngine;
using System.Collections;

public class Sparks : MonoBehaviour {

    public Transform particles;
    GameObject spark;

    void OnCollisionEnter2D(Collision2D coll)
    {

        spark = (GameObject) Instantiate(Resources.Load<GameObject>("SparkGen"), coll.contacts[0].point ,transform.rotation);
        spark.GetComponent<ParticleSystem>().startSize = coll.relativeVelocity.magnitude / 20;
        spark.transform.parent = particles;
    }
}
