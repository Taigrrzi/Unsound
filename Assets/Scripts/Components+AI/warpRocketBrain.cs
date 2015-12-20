using UnityEngine;
using System.Collections;

public class warpRocketBrain : MonoBehaviour {

    public int boundKey;
    public int warpTime;
    public int warpTimer;
    public int chargeIncrease;
    public shipComputer computer;
    public int baseChargeRate;
    public int chargeDecay;
    public int currentCharge;
    public int fullCharge;
    public int breakCharge;
    public bool charged;
    public Console console;

	// Use this for initialization
	void Start () {
        charged = false;
        currentCharge = Mathf.FloorToInt(fullCharge*(3/4)) ;
        transform.GetChild(1).gameObject.SetActive(false);
        chargeIncrease = 0;
        computer = transform.parent.GetComponent<shipComputer>();
        console = GameObject.Find("ShipConsole").GetComponent<Console>();
        console.AddFunctionality(new ConsoleFunctionality("chargewarp", gameObject,"Chargewarp:: No Paremeters. Begins charging the ship warp engine.",true));
        console.AddFunctionality(new ConsoleFunctionality("dischargewarp", gameObject, "Dischargewarp:: No Paremeters. Stops charging the ship warp engine.", true));

        console.AddFunctionality(new ConsoleFunctionality("warp", gameObject, "Warp:: No Paremeters. Activates the ship warp engine.", true));
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<ConsoleReciever>().incomingFunction)
        {
            HandleFunction();
        }
        currentCharge += chargeIncrease - chargeDecay;
        if (currentCharge<0)
        {
            currentCharge = 0;
        }
        if (currentCharge>fullCharge)
        {
            charged = true;
        } else
        {
            charged = false;
        }
        if (currentCharge>fullCharge)
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(2).gameObject.SetActive(true);
        } else if (currentCharge<fullCharge)
        {
            transform.GetChild(2).gameObject.SetActive(false);
        }
	}

    public void HandleFunction()
    {
        string funcString = GetComponent<ConsoleReciever>().functionName;
        switch (funcString)
        {
            case "chargewarp":
                console.LogToConsole("Warp Engine Charging");
                if (chargeIncrease != baseChargeRate)
                {
                    transform.GetChild(1).gameObject.SetActive(true);
                    chargeIncrease = baseChargeRate;
                    console.LogToConsole("Warp Engine Now Charging");
                }
                else
                {
                    console.LogToConsole("Cannot Activate An Active Warp Engine");
                }
                break;
            case "dischargewarp":
                if (chargeIncrease != 0)
                {
                    transform.GetChild(1).gameObject.SetActive(false);
                    chargeIncrease = 0;
                    console.LogToConsole("Warp Engine No Longer Charging");
                } else
                {
                    console.LogToConsole("Cannot Deactivate An Inactive Warp Engine");
                }
                break;
            case "warp":
                {
                    if (currentCharge>fullCharge)
                    {
                        Application.Quit();
                        console.LogToConsole("Warping");
                    } else
                    {
                        console.LogToConsole("Warp Engine Not Charged");
                        currentCharge = 0;
                    }
                }
                break;
            default:
                Debug.LogError("HandleFunction called with invalid function name! on "+name);
                break;
        }
        GetComponent<ConsoleReciever>().incomingFunction = false;
    }
}
