using UnityEngine;
using System.Collections;

public class Sparks : MonoBehaviour {

    GameObject spark;

    void OnCollisionExit2D(Collision2D coll)
    {

        spark = (GameObject) Instantiate(Resources.Load<GameObject>("SparkGen"), coll.contacts[0].point ,transform.rotation);
    }
}
