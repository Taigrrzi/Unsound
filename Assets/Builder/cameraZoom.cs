using UnityEngine;
using System.Collections;

public class cameraZoom : MonoBehaviour
{

    public float zoomSpeed;
    public float intendedZoom;
    Vector3 mouseWorld;

    void Start()
    {
        mouseWorld = transform.position;
    }

    void FixedUpdate()
    {
        intendedZoom -= zoomSpeed * Input.GetAxis("MouseScrollWheel") * intendedZoom;
        if (intendedZoom < 100)
        {
            intendedZoom = 100;
        }
        if (Input.GetAxis("MouseScrollWheel") != 0)
        {
            mouseWorld = GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);
        }
        transform.position = Vector3.Lerp(transform.position, mouseWorld, zoomSpeed * Input.GetAxis("MouseScrollWheel"));
        GetComponent<Camera>().orthographicSize = Mathf.Lerp(GetComponent<Camera>().orthographicSize, intendedZoom, 0.2f);
    }
}