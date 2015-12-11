using UnityEngine;
using System.Collections;
[System.Serializable]
public class ConsoleFunctionality {

    public string consoleInitiateString;
    public GameObject objectWithFunction;
    public string helpString;
    public bool nonHidden;

    public ConsoleFunctionality(string cis,GameObject owf,string hs,bool nh) 
    {
        consoleInitiateString = cis;
        objectWithFunction = owf;
        helpString = hs;
        nonHidden = nh;
    }
}
