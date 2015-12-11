using UnityEngine;
using System.Collections;

public class ConsoleReciever : MonoBehaviour {

    public bool incomingFunction;
    public string functionName;
    public string[] consoleInput;

    public void FunctionCalled(string fn,string[] ci)
    {
        incomingFunction = true;
        functionName = fn;
        consoleInput = ci;
    }
}
