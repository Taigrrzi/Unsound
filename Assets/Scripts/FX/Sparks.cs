using UnityEngine;
using System.Collections;

public class Sparks : MonoBehaviour {

    GameObject spark;
    Transform FX;

    void Start()
    {
        FX = GameObject.Find("FX").transform;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {

        spark = (GameObject) Instantiate(Resources.Load<GameObject>("SparkGen"), coll.contacts[0].point ,transform.rotation);
        spark.transform.parent = FX;
        spark.GetComponent<ParticleSystem>().startSize = coll.relativeVelocity.magnitude / 20;
    }
}
