using UnityEngine;
using System.Collections;

public class speechController : MonoBehaviour {

    public Person p;
    public int currentResponse;
    public GameObject ResponseA;
    public GameObject ResponseB;


    void Update () {
        GetComponent<UnityEngine.UI.Text>().text = p.conversation[currentResponse].message[p.manner];

        ResponseA.GetComponent<UnityEngine.UI.Text>().text = p.conversation[currentResponse].outText[0];
        ResponseB.GetComponent<UnityEngine.UI.Text>().text = p.conversation[currentResponse].outText[1];
    }

    public void ProgressConvoA()
    {
        currentResponse = p.conversation[currentResponse].outIndex[0];

        p.lImpression += p.conversation[currentResponse].lImpEffect;
        p.rImpression += p.conversation[currentResponse].rImpEffect;

    }

    public void ProgressConvoB()
    {
        currentResponse = p.conversation[currentResponse].outIndex[1];

        p.lImpression += p.conversation[currentResponse].lImpEffect;
        p.rImpression += p.conversation[currentResponse].rImpEffect;

    }
}
