using UnityEngine;
using System.Collections;
[System.Serializable]
public class Response : System.Object {
    public string[] message;
    public float lImpEffect;
    public float rImpEffect;
    public int[] outIndex;
    public string[] outText;

    public Response(string[] m, float l, float r, int[] outI, string[] outT)
    {
        message = m;
        lImpEffect = l;
        rImpEffect = r;
        outIndex = outI;
        outText = outT;
    }
}
