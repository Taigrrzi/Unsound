using UnityEngine;
using System.Collections;

public class drawArrow : MonoBehaviour {

    public Vector3 start;
    public Vector3 end;
    public GameObject A;
    public GameObject B;

    Transform child;
    LineRenderer lrender;
    float direction;

	// Use this for initialization
	void Start () {
        lrender = GetComponent<LineRenderer>();
        lrender.SetWidth(5, 2);
        
        child = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update () {
        if (A != null)
        {
            start = A.transform.position;
        }
        if (B != null)
        {
            end = B.transform.position + Vector3.back;
        }

        child.position = (start + end) / 2;

        lrender.SetPosition(0, start);
        lrender.SetPosition(1, end);
        transform.position = end;

        direction = Vector3.Angle(Vector3.up, end - start);

        transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Sign((end-start).x) * -direction));
    }
}
