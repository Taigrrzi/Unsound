using UnityEngine;
using System.Collections;

public class RocketBrain : MonoBehaviour {

    public bool active;
    public float thrustPower;
    //public byte rocketDirection; //0 = right, 1 = down, 2 = left, 3 = up
    public float myRotation;
    public float xComponent;
    public float yComponent;
    public float xDistance;
    public float yDistance;
    // Update is called once per frame
    void Start()
    {
        yDistance = transform.localPosition.y - transform.parent.position.y;
        xDistance = transform.localPosition.x - transform.parent.position.x;
        xComponent = -Mathf.Sin(Mathf.Deg2Rad * myRotation);
        yComponent = Mathf.Cos(Mathf.Deg2Rad * myRotation);
    }

    void Update () {
        myRotation = transform.parent.rotation.eulerAngles.z;
        GetComponent<Animator>().SetBool("RocketActive",active);
        if (active)
        {
            transform.parent.GetComponent<Rigidbody2D>().AddRelativeForce(new Vector2(thrustPower*xComponent,thrustPower*yComponent),ForceMode2D.Force);
            transform.parent.GetComponent<Rigidbody2D>().AddTorque(0.1f*((thrustPower*xComponent*yDistance) + (thrustPower * yComponent * xDistance)));
        }
	}
}
