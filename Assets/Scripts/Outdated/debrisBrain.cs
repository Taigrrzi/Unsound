using UnityEngine;
using System.Collections;

public class debrisBrain : MonoBehaviour {  

    void Start()
    {
        float rand1 = Random.Range(0.5f, 2.5f);
        float rand2 = Random.Range(0.5f, 2.5f);
        transform.localScale = new Vector3(rand1,rand2, 0);
        GetComponent<Rigidbody2D>().mass = rand1 * rand2 * GetComponent<Rigidbody2D>().mass;
    }
}
