using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Console : MonoBehaviour
{
    public List<string> consoleStrings ;
    public string typedText;
    public bool selected;
    public bool textEntered;
    public int pastFocus;
    public List<ConsoleFunctionality> functionalities ;
    public List<string> pastFunctions;
    public GameObject playerShip ;
    public GameObject canvas;

    void Awake()
    {
        Hide();
        pastFunctions = new List<string>();
        consoleStrings = new List<string>();
        functionalities = new List<ConsoleFunctionality>();
        selected = false;
    }

    void Start()
    {
        AddFunctionality(new ConsoleFunctionality("close",gameObject,"Close:: No Parameters. Closes the console window.",true));
        AddFunctionality(new ConsoleFunctionality("help", gameObject, "Help:: String functionName. Very funny, shows the details of a console function.",true));
        AddFunctionality(new ConsoleFunctionality("list", gameObject, "List:: Shows a list of all public functions useable in the console",true));
    }

    void Update()
    {
        if (GetComponent<ConsoleReciever>().incomingFunction)
        {
            HandleFunction();
        }
        if (textEntered)
        {
            textEntered = false;
        }
        if (selected)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (pastFocus < pastFunctions.Count-1) {
                    pastFocus++;
                    typedText = pastFunctions[pastFocus];
                }
            }  else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (pastFocus > 0)
                {
                    pastFocus--;
                    typedText = pastFunctions[pastFocus];
                }
            }
            foreach (char c in Input.inputString)
            {
                if (c == "\b"[0])
                {
                    if (typedText.Length > 0)
                    {
                        typedText = typedText.Substring(0, typedText.Length - 1);
                    }
                }
                else
                {
                    if (c == "\n"[0] || c == "\r"[0])
                    {
                       pastFocus = 0;
                       textEntered = true;
                       selected = false;
                       Parse(typedText);
                       typedText = "";
                    }
                    else
                    {
                        typedText += c;
                    }
                }
            }
        }
        transform.GetChild(2).GetComponent<Text>().text = typedText;
        transform.GetChild(1).GetComponent<Text>().text = "";
        foreach (string historyLine in consoleStrings)
        {
            transform.GetChild(1).GetComponent<Text>().text += "\n"+historyLine;
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (!selected)
            {
                selected = true;
            }
        }
    }

    public void Hide()
    {
        transform.SetParent(null);
        GetComponent<dragPanel>().enabled = false;
        selected = false; 
    }

    public void Show()
    {
        transform.SetParent(canvas.transform);
        GetComponent<dragPanel>().enabled = true;
        selected = true;
        typedText = "";
    }

    public void LogToConsole(string logString)
    {
        consoleStrings.Add(logString);
    }

    public static void LogToShipConsole(string logString)
    {
        GameObject.Find("ShipConsole").GetComponent<Console>().consoleStrings.Add(logString);
    }

    public void AddFunctionality(ConsoleFunctionality newFunc)
    {
        functionalities.Add(newFunc);
        LogToConsole("Added Functionality: "+newFunc.helpString);
    }

    public void RemoveFunctionality(ConsoleFunctionality newFunc)
    {
        if (functionalities.Contains(newFunc))
        {
            functionalities.Remove(newFunc);
        } else
        {
            Debug.LogError("Trying to remove console functionality that doesn't exist!");
        }
    }

    public void Parse(string text)
    {
        pastFunctions.Add(text);
        string[] splitText = text.Split(';');
        string[] firstCommand = splitText[0].Split(' ');
        bool foundFunction = false;
        foreach (ConsoleFunctionality func in functionalities)
        {
            if (firstCommand[0] == func.consoleInitiateString)
            {
                if (!foundFunction)
                {
                    foundFunction = true;
                    func.objectWithFunction.GetComponent<ConsoleReciever>().FunctionCalled(func.consoleInitiateString, firstCommand);
                } else
                {
                    Debug.LogError("Two console functionalities exist with the same function string!");
                }
            }
        }
        if (!foundFunction)
        {
            LogToConsole("Unknown Function Entered: "+text);
        }
        //LogToConsole(text);
        if (splitText.Length > 1)
        {
            text = "";
            for (int i = 1; i < splitText.Length; i++)
            {
                text += splitText[i];
            }
            Parse(text);
        }
    }

    public void HandleFunction()
    {
        string[] consoleInput = GetComponent<ConsoleReciever>().consoleInput;
        string funcString = GetComponent<ConsoleReciever>().functionName;
        switch (funcString)
        {
            case "close":
                Close();
                break;
            case "list":
                LogPublicFunctions();
                break;
            case "help":
                if (consoleInput.Length > 1)
                {
                    Help(consoleInput[1]);
                } else
                {
                    LogToConsole("Please specify a function for help function");
                }
                break;
            default:
                Debug.LogError("HandleFunction called with invalid function name!");
                break;
        }
        GetComponent<ConsoleReciever>().incomingFunction = false;
    }

    public void Close()
    {
        Hide();
    }

    public void Help(string funcString)
    {
        foreach (ConsoleFunctionality func in functionalities)
        {
            if (func.consoleInitiateString == funcString)
            {
                LogToConsole(func.helpString);
                return;
            }
        }
        LogToConsole("No Such Function: "+funcString);
        return;
    }

    public void LogPublicFunctions()
    {
        LogToConsole("Function List: ");
        foreach (ConsoleFunctionality func in functionalities)
        {
            if (func.nonHidden)
            {
                LogToConsole(func.consoleInitiateString);
            }
        }
    }
}
