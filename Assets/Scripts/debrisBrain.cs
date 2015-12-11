using UnityEngine;
using System.Collections;

public class debrisBrain : MonoBehaviour {

    public Transform chunkDrawing;
    Vector2 loadedVelocity;
    float loadedAngularVelocity;
    //chunkDrawing chunkDrawingScript;

    // Use this for initialization
    void Start () {
        //chunkDrawingScript = chunkDrawing.GetComponent<chunkDrawing>();
        //SaveVeloc();
    }

   // void Update ()
    //{

        //transform.parent = chunkDrawingScript.ChunkAt(transform);
        //    if (transform.parent==null)
        //{
        //    transform.parent = chunkDrawingScript.GenerateChunk(transform);
        //}
    //}

    /*public void SaveVeloc() {
        loadedVelocity = GetComponent<Rigidbody2D>().velocity;
        loadedAngularVelocity = GetComponent<Rigidbody2D>().angularVelocity;
    }*/

    /*public void LoadVeloc()
    {
        GetComponent<Rigidbody2D>().velocity = loadedVelocity;
        GetComponent<Rigidbody2D>().angularVelocity = loadedAngularVelocity;
    }*/
    /*
    void OnDisable()
    {
        //Debug.Log("Disabled");
        loadedVelocity = GetComponent<Rigidbody2D>().velocity;
        loadedAngularVelocity = GetComponent<Rigidbody2D>().angularVelocity;
    }

    void OnEnable()
    {
        GetComponent<Rigidbody2D>().velocity = loadedVelocity;
        //if (loadedVelocity==Vector2.zero)
        //{
         //   Debug.Log("Reloaded Not Moving");
        //}
        GetComponent<Rigidbody2D>().angularVelocity = loadedAngularVelocity;
    }*/
}
