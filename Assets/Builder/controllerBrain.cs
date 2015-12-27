using UnityEngine;
using System.Collections;

public class controllerBrain : MonoBehaviour {

    GameObject newField;
    public ArrayList dialogue;
    public ArrayList connections;
    public bool attaching;

    public GameObject currentArrow;

    public GameObject A;
    public GameObject B;

    Vector3 APoint;
    Vector3 BPoint;

	// Use this for initialization
	void Start () {
        dialogue = new ArrayList();
        connections = new ArrayList();
        attaching = false;
        CreateResponse();
	}
	
	// Update is called once per frame
	void Update () {
        if (attaching)
        {
            if (B == null)
            {
                currentArrow.GetComponent<drawArrow>().end = Camera.main.ScreenToWorldPoint(Input.mousePosition) + (Vector3.forward * 9);
            } else
            {
                currentArrow.GetComponent<drawArrow>().B = B;
                if (B == A)
                {
                    Destroy(currentArrow);
                    connections.Remove(currentArrow);
                }
                currentArrow = null;
                B = null;
                attaching = false;
            }
        }
    }

    

    public void CreateResponse ()
    {
        newField = (GameObject) Instantiate(Resources.Load("CharacterResponse"),new Vector3(0,0,0), transform.rotation);
        newField.transform.SetParent(FindObjectOfType<Canvas>().transform);
        dialogue.Add(newField);
    }
}
