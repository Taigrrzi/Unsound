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

    void Start () {
        computerName = "ZHI Computer";
        panelOut = false;
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
    }
}
