using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class spaceStationBrain : MonoBehaviour {

    public int triggerZoneAmount = 1;
    public GameObject[] triggerZones;
    private float[,] TriggerZoneCreationDetails = new float[1, 4];
    public bool basePanelOut = false;
    public string stationName = "SS Resolute";
    public int size; //0 = small, 1= medium, 2= large, 3=huge
    public int crewSize;
    public Person leader;
    public GameObject panel;


    // Use this for initialization
    void Start () {
        leader = new Person(Random.value);
        crewSize = (int)(Random.value * (Mathf.Pow(10,size)));
        triggerZones = new GameObject[triggerZoneAmount];
        TriggerZoneCreationDetails[0, 0] = 1.34f;
        TriggerZoneCreationDetails[0, 1] = 0.985f;
        TriggerZoneCreationDetails[0, 2] = 0.58f;
        TriggerZoneCreationDetails[0, 3] = 0.63f;
        for (int i = 0; i < triggerZoneAmount; i++) 
        {
            triggerZones[i] = new GameObject();
            triggerZones[i].name = "Access Zone: "+i.ToString();
            triggerZones[i].transform.parent = transform;
            triggerZones[i].transform.localScale = new Vector3(1,1,1);
            triggerZones[i].AddComponent<BoxCollider2D>();
            triggerZones[i].AddComponent<triggerZone>();
            triggerZones[i].transform.localPosition = new Vector3(TriggerZoneCreationDetails[i, 0], TriggerZoneCreationDetails[i, 1], 0);
            triggerZones[i].GetComponent<BoxCollider2D>().size = new Vector2(TriggerZoneCreationDetails[i, 2], TriggerZoneCreationDetails[i, 3]);
            triggerZones[i].GetComponent<BoxCollider2D>().isTrigger = true;
        }


	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (GetComponent<Collider2D>().OverlapPoint(mousePosition) && !basePanelOut)
            {
                panel = Instantiate(Resources.Load<GameObject>("StationPanel"));
                panel.GetComponent<StationPanel>().station = gameObject;
                basePanelOut = true;
                panel.transform.position = Camera.main.WorldToScreenPoint(transform.GetChild(0).position);
                panel.transform.SetParent(GameObject.Find("Canvas").transform);
                panel.transform.FindChild("CloseButton").GetComponent<Button>().onClick.AddListener(delegate { ClosePanel(); });
                panel.SetActive(true);
            }
        }
    }

    public void ClosePanel()
    {
        basePanelOut = false;
        Destroy(panel);
    }

}
