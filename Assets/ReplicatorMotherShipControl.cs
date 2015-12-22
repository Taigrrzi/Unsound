using UnityEngine;
using System.Collections;

public class ReplicatorMotherShipControl : MonoBehaviour {

    public GameObject[] segments;
    public float switchDelay;
    float switchTimer;
	// Use this for initialization
	void Start () {
        switchTimer = switchDelay;
        segments = new GameObject[4];
	    for (int i=0;i<4; i++)
        {
            segments[i] = transform.GetChild(i + 1).gameObject;
            segments[i].GetComponent<replicatorBulwarkControl>().shipControl = gameObject.GetComponent<ReplicatorMotherShipControl>();
        }
	}
	
	// Update is called once per frame
	void Update () {
        switchTimer+=Time.deltaTime;
        if (switchTimer>switchDelay)
        {
            int rand1 = ZeroOrTwo()-1;
            for (int i = 0; i < 4; i++)
            {
                segments[i].GetComponent<replicatorBulwarkControl>().desiredState = 0;
            }
            if (Random.value>=0.5)
            {
                int rand = ZeroOrTwo();
                segments[0 + rand].GetComponent<replicatorBulwarkControl>().desiredState = rand1;
                segments[1 + rand].GetComponent<replicatorBulwarkControl>().desiredState = rand1;
            } else
            {
                int rand = Mathf.FloorToInt(Random.value*2);
                segments[0 + rand].GetComponent<replicatorBulwarkControl>().desiredState = rand1;
                segments[2 + rand].GetComponent<replicatorBulwarkControl>().desiredState = rand1;
            }
            switchTimer = 0;
        }
	}

    public int ZeroOrTwo()
    {
        if (Random.value > 0.5)
        {
            return 2;
        }
        else
        {
            return 0;
        }
    }
}
