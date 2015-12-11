using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
public class shipComputer : MonoBehaviour {

    public GameObject[] attachedComponents;
    public int maxBindings = 8;
    public KeyCode[] keyBindingCodes;
    public string[] keyBindingNames;
    public bool[] keyBindingStates;
    public bool panelOut;
    public int usedKeyBindings;
    public string computerName;
    public int baseHullArmor;
    public bool tagging;
    public int currentHullArmor;
    public bool consolePanelOut;
    public int maxTaggedObjects;
    public int TaggedObjectsUsed;
    public GameObject[] taggedObjects;
    public Console console;

    void Start () {
        console.AddFunctionality(new ConsoleFunctionality("ping", gameObject, "Ping:: No Parameters. Sends out a short radio ping.",true));
        console.AddFunctionality(new ConsoleFunctionality("freeze", gameObject, "Freeze:: No Parameters. Stops ship movement.",true));
        console.AddFunctionality(new ConsoleFunctionality("noclip", gameObject, "Noclip:: Toggles Ship Colliders",false));

        taggedObjects = new GameObject[maxTaggedObjects];
        currentHullArmor = baseHullArmor;
        computerName = "ZHI Computer";
        panelOut = false;
        consolePanelOut = false;
        usedKeyBindings = 0;
        keyBindingNames = new string[maxBindings];
        keyBindingCodes = new KeyCode[maxBindings];
        keyBindingStates = new bool[maxBindings];
        keyBindingNames[0] = "Forward";
        keyBindingNames[1] = "Backward";
        keyBindingNames[2] = "TurnLeft";
        keyBindingNames[3] = "TurnRight";
        keyBindingNames[4] = "StrafeLeft";
        keyBindingNames[5] = "StrafeRight";
        keyBindingCodes[0] = KeyCode.W;
        keyBindingCodes[1] = KeyCode.S;
        keyBindingCodes[2] = KeyCode.A;
        keyBindingCodes[3] = KeyCode.D;
        keyBindingCodes[4] = KeyCode.Q;
        keyBindingCodes[5] = KeyCode.E;
        usedKeyBindings = 6;
        attachedComponents = new GameObject[transform.childCount];
        for (int i = 0; i<transform.childCount; i++)
        {
            attachedComponents[i] = transform.GetChild(i).gameObject;
        }

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
        for (int i=0;i<usedKeyBindings;i++)
        {
            keyBindingStates[i] = Input.GetKey(keyBindingCodes[i]);
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (GetComponent<Collider2D>().OverlapPoint(mousePosition) && !panelOut)
            {
                //Debug.Log(name + "was Clicked");
                GameObject panel = Instantiate(Resources.Load<GameObject>("ShipComputerPanel"));
                panel.GetComponent<shipComputerPanel>().computer = gameObject;
                panelOut = true;
                panel.transform.position = Camera.main.WorldToScreenPoint(transform.position);
                panel.transform.SetParent(GameObject.Find("Canvas").transform);
            }


        }
        for (int i = 0; i<TaggedObjectsUsed;i++)
        {
            if (taggedObjects[i]!=null)
            {

            }else
            {
                for (int j = i; j < TaggedObjectsUsed-1; j++)
                {
                    taggedObjects[j] = taggedObjects[j + 1];
                }
                taggedObjects[maxTaggedObjects] = null;
            }
        }

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        currentHullArmor -= Mathf.RoundToInt(collision.relativeVelocity.magnitude*10);
    }

    public void addNewTag(GameObject taggedObj)
    {
        if (TaggedObjectsUsed<maxTaggedObjects)
        {
            taggedObjects[TaggedObjectsUsed] = taggedObj;
            TaggedObjectsUsed++;
        } else
        {
            Debug.LogError("Called Object Tag function and computer has no more space!");
        }
    }

    public void HandleFunction()
    {
        string[] consoleInput = GetComponent<ConsoleReciever>().consoleInput;
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

}
