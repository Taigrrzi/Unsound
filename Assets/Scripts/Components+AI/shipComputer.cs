﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class shipComputer : MonoBehaviour {
    public List<GameObject> attachedComponents;
    public Dictionary<GameObject,GameObject> shipBuildingZones;
    public List<GameObject> taggedObjects;
    public bool panelOut;
    public Transform shipBuildingParent;
    public string computerName;
    public int baseHullArmor;
    public bool tagging;
    public float killRotation;
    public int currentHullArmor;
    public bool consolePanelOut;
    public float rotTimer;
    public Console console;
    public GameObject panel = null;
    public GameObject dPanel;
    public int[] ammoAmounts; //1 = garbage
    public float rotationLastTurn;

    void Start () {
        shipBuildingParent = transform.FindChild("ShipBuildingParent");
        rotationLastTurn = 0;
        ammoAmounts = new int[2];
        ammoAmounts[1] = 100;
        console.AddFunctionality(new ConsoleFunctionality("ping", gameObject, "Ping:: No Parameters. Sends out a short radio ping.",true));
        console.AddFunctionality(new ConsoleFunctionality("freeze", gameObject, "Freeze:: No Parameters. Stops ship movement.",true));
        console.AddFunctionality(new ConsoleFunctionality("noclip", gameObject, "Noclip:: Toggles Ship Colliders",false));

        currentHullArmor = baseHullArmor;
        computerName = "ZHI Computer";
        panelOut = false;
        consolePanelOut = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (consolePanelOut)
            {
                consolePanelOut = false;
                console.Hide();
            } else
            {
                consolePanelOut = true;
                console.Show();
            }
        }
        if (GetComponent<ConsoleReciever>().incomingFunction)
        {
            HandleFunction();
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (GetComponent<Collider2D>().OverlapPoint(mousePosition) && !panelOut)
            {
                //Debug.Log(name + "was Clicked");
                panel = Instantiate(Resources.Load<GameObject>("ShipComputerPanel"));
                panel.GetComponent<shipComputerPanel>().computer = gameObject;
                panelOut = true;
                panel.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                panel.transform.SetParent(GameObject.Find("Canvas").transform);
                panel.transform.FindChild("CloseButton").GetComponent<Button>().onClick.AddListener(delegate { ClosePanel(); });
                panel.SetActive(true);
            }


        }
    }

    void FixedUpdate()
    {
        rotTimer += Time.deltaTime;
        if (rotTimer > 0.2  )
        {
            rotationLastTurn = Mathf.Abs(GetComponent<Rigidbody2D>().angularVelocity);
            rotTimer = 0;
        }
        if (Mathf.Abs(GetComponent<Rigidbody2D>().angularVelocity) < killRotation && Mathf.Abs(GetComponent<Rigidbody2D>().angularVelocity) < rotationLastTurn)
        {
            GetComponent<Rigidbody2D>().angularVelocity = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        currentHullArmor -= Mathf.RoundToInt(collision.relativeVelocity.magnitude*10);
    }

    public void HandleFunction()
    {
        string funcString = GetComponent<ConsoleReciever>().functionName;
        switch (funcString)
        {
            case "ping":
                Ping();
                break;
            case "noclip":
                ToggleCollision();
                console.LogToConsole("Ship Collision Toggled");
                break;
            case "freeze":
                GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                GetComponent<Rigidbody2D>().angularVelocity = 0;
                console.LogToConsole("Ship Stopped");
                break;
            default:
                Debug.LogError("HandleFunction called with invalid function name!");
                break;
        }
        GetComponent<ConsoleReciever>().incomingFunction = false;
    }

    public void Ping()
    {
        GameObject pingObj = Instantiate(Resources.Load<GameObject>("Ping"));
        pingObj.transform.position = transform.position;
        console.LogToConsole("Radio Ping Launched");
        //pingObj.GetComponent<pingBrain>().followobject = gameObject;
    }

    public void ToggleCollision()
    {
        foreach (Collider2D col in GetComponentsInChildren<Collider2D>())
        {
            col.enabled = !(col.enabled);
        }
    }

    public void ClosePanel()
    {
        panelOut = false;
        Destroy(panel);
    }

}
