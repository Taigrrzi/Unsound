using UnityEngine;
using System.Collections;

public class cameraFollow : MonoBehaviour
{

    public GameObject target;
    public float zoomSpeed;
    public float intendedZoom;
    public Vector2 mouseScreenPosition;
    public Vector2 mouseWorldPosition;
    public Vector3 intendedPosition;
    public bool snapRotation;
    // Use this for initialization
    // Update is called once per frame
    void FixedUpdate()
    {
        if (snapRotation)
        {
            transform.rotation = target.transform.rotation; 
        }
        intendedZoom -= zoomSpeed * Input.GetAxis("MouseScrollWheel");
        if (intendedZoom < 1)
        {
            intendedZoom = 1;
        }
        if (intendedZoom > 10)
        {
            intendedZoom = 10;
        }
        //transform.position = new Vector3 (target.transform.position.x, target.transform.position.y, Mathf.Lerp(transform.position.z,intendedZoom,0.2f));
        if (Input.GetButton("Look"))
        {
            Debug.Log(Input.GetAxis("MouseScrollWheel"));
            mouseScreenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(mouseScreenPosition.x, mouseScreenPosition.y, 0f));
            intendedPosition = new Vector3((target.transform.position.x + mouseWorldPosition.x) / 2, (target.transform.position.y + mouseWorldPosition.y) / 2, -1);
        }
        else
        {
            intendedPosition = new Vector3(target.transform.position.x, target.transform.position.y, -1);
        }

        transform.position = Vector3.Lerp(transform.position, intendedPosition, Mathf.Abs(((17 - intendedZoom) / 16) - (intendedZoom / (intendedZoom + 1)) / 4) + 0.1f);
        //transform.position = intendedPosition ;
        //Mathf.Abs(((17-intendedZoom)/16)-(intendedZoom/(intendedZoom+1))/2)
        //Debug.Log (GetComponent<Camera> ().orthographicSize);
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, intendedZoom, 0.2f);
    }
}