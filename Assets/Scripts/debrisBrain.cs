using UnityEngine;
using System.Collections;

public class debrisBrain : MonoBehaviour {

    public Transform chunkDrawing;
    int gridSize;
    int chunkSize;

    // Use this for initialization
    void Start () {
        chunkDrawing = GameObject.Find("Chunk Based Drawing").transform;
        gridSize = chunkDrawing.GetComponent<chunkDrawing>().gridSize;
        chunkSize = chunkDrawing.GetComponent<chunkDrawing>().chunkSize;
        GetComponent<Rigidbody2D>().angularVelocity = Random.value*100;
        GetComponent<Rigidbody2D>().velocity = Vector2.right*((Random.value*2)-1) ;
    }

    void Update ()
    {
        
        transform.parent = chunkDrawing.transform.FindChild("Chunk :" + (Mathf.FloorToInt(transform.position.x / chunkSize) + (gridSize / 2)) + "," + (Mathf.FloorToInt(transform.position.y / chunkSize) + (gridSize / 2)));
        if (transform.parent==null)
        {
            transform.parent = chunkDrawing.GetComponent<chunkDrawing>().GenerateEmpty(transform.position);
        }
    }
}
