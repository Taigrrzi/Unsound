﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StationPanel : MonoBehaviour
{

    public GameObject station;
    public Vector3 myWorldPos;
    public Vector3 myWorldPosPlusHeight;

    void Start()
    {
        transform.GetChild(0).GetComponent<Text>().text = station.GetComponent<spaceStationBrain>().stationName;
       
    }

    void Update()
    {
        // Vector3 worldPoint = Camera.main.ScreenToWorldPoint(transform.position);
        LineRenderer line1 = transform.GetChild(1).GetComponent<LineRenderer>();
        LineRenderer line2 = transform.GetChild(2).GetComponent<LineRenderer>();

        myWorldPos = Camera.main.ScreenToWorldPoint(transform.position);
        myWorldPos = new Vector3(myWorldPos.x, myWorldPos.y, 0f);
        myWorldPosPlusHeight = Camera.main.ScreenToWorldPoint(transform.position + Vector3.down * GetComponent<RectTransform>().sizeDelta.y);
        myWorldPosPlusHeight = new Vector3(myWorldPosPlusHeight.x, myWorldPosPlusHeight.y, 0f);
        //line1.SetPosition(0, Camera.main.ScreenToWorldPoint(transform.position));
        line1.SetPosition(0, myWorldPos);
        line1.SetPosition(1, station.transform.GetChild(0).position);
        line2.SetPosition(0, myWorldPosPlusHeight);
        line2.SetPosition(1, station.transform.GetChild(0).position);
        line1.material = new Material(Shader.Find("Particles/Additive"));
        line2.material = new Material(Shader.Find("Particles/Additive"));
        line1.SetWidth(Camera.main.GetComponent<Camera>().orthographicSize * 0.01f, Camera.main.GetComponent<Camera>().orthographicSize * 0.01f);
        line2.SetWidth(Camera.main.GetComponent<Camera>().orthographicSize * 0.01f, Camera.main.GetComponent<Camera>().orthographicSize * 0.01f);

    }
}
