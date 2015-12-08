using UnityEngine;
using System.Collections;

public class chunkDrawing : MonoBehaviour {
    public int gridSize= 100;
    public int chunkSize = 50;
    public bool[,] chunksDrawn = new bool[0,0];
    public Coord currentChunk;
    public GameObject playerShip;

    // Use this for initialization
    void Start () {
        chunksDrawn = new bool[gridSize, gridSize];
        for (int i=0; i< gridSize;i++)
        {
            for (int j=0; j< gridSize; j++)
            {
                chunksDrawn[i, j] = false;
            }
        }
        currentChunk.x = Mathf.FloorToInt(playerShip.transform.position.x / chunkSize) + (gridSize / 2);
        currentChunk.y = Mathf.FloorToInt(playerShip.transform.position.y / chunkSize) + (gridSize / 2);
        GenerateChunksNine(currentChunk);

    }
	
	// Update is called once per frame
	void Update () {
        currentChunk.x = Mathf.FloorToInt(playerShip.transform.position.x/ chunkSize) + (gridSize/2);
        currentChunk.y = Mathf.FloorToInt(playerShip.transform.position.y/ chunkSize) + (gridSize/2);
        GenerateMissingNearby(currentChunk);

        for (int i=0; i<transform.childCount;i++)
        {
            if (transform.GetChild(i).tag == "Group")
            {
                string[] splitted = transform.GetChild(i).name.Split(',');
               // Debug.Log(splitted[0].Substring(7));
                float chunkx = int.Parse(splitted[0].Substring(7));
                float chunky = int.Parse(splitted[1]);
                if (Mathf.Sqrt(Mathf.Pow(chunkx - currentChunk.x, 2) + Mathf.Pow(chunky - currentChunk.y, 2)) > 1.7)
                {
                    disableChunk(transform.GetChild(i));
                }
                else
                {
                    enableChunk(transform.GetChild(i));
                }
            }
        }
    }

    float toChunkValue(float worldvalue)
    {
        return (Mathf.FloorToInt(worldvalue / chunkSize) + (gridSize / 2));
    }

    float toWorldValue(float chunkvalue)
    {
        return ((chunkvalue-(gridSize / 2))*chunkSize);
    }

    void disableChunk(Coord chunkPos)
    {
        if (transform.FindChild("Chunk :" + chunkPos.x + "," + chunkPos.y)!=null) {
            Transform chunkObj = transform.FindChild("Chunk :" + chunkPos.x + "," + chunkPos.y) ;
            chunkObj.gameObject.SetActive(false);
        } else
        {
            Debug.LogError("Trying to disable non-existant chunk!");
        }
    }

    void disableChunk(Transform chunkObj)
    {
        //Transform chunkObj = transform.FindChild("Chunk :" + chunk.position.x + "," + chunk.position.y);
        chunkObj.gameObject.SetActive(false);
    }

    void enableChunk(Transform chunkObj)
    {
        //Transform chunkObj = transform.FindChild("Chunk :" + chunk.position.x + "," + chunk.position.y);
        chunkObj.gameObject.SetActive(true);
    }

    void enableChunk(Coord chunkPos)
    {
        Transform chunkObj = transform.FindChild("Chunk :" + chunkPos.x + "," + chunkPos.y);
        if (chunkObj != null)
        {
            chunkObj.gameObject.SetActive(true);
        }
        else
        {
            Debug.LogError("Trying to enable non-existant chunk!");
        }
    }

    void GenerateChunksSurrounding(Coord chunkPos)
    {
        for (int i=0; i<3; i++)
        {
            for (int j=0; j<3; j++)
            {
                if (i!=1||j!=1)
                {
                    GenerateChunk(new Coord(chunkPos.x+i-1, chunkPos.y + j - 1));
                }
            }
        }
    }

    void GenerateChunksNine(Coord chunkPos)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                    GenerateChunk(new Coord(chunkPos.x + i - 1, chunkPos.y + j - 1));
            }
        }
    }
    
    void GenerateChunksList(Coord[] chunks)
    {
        for (int i=0; i<chunks.Length; i++)
        {
            GenerateChunk(chunks[i]);
        }
    }

    void GenerateMissingNearby(Coord chunkPos)
    {
       // Coord[] missingList = new Coord[9];
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (!chunksDrawn[(int)chunkPos.x + i - 1, (int)chunkPos.y + j - 1])
                {
                    GenerateChunk(new Coord(chunkPos.x + i - 1, chunkPos.y + j - 1));
                }
            }
        }
    }

    public Transform GenerateEmpty(Vector3 pos) 
    {
        GameObject chunk = new GameObject();
        chunk.name = "Chunk :" + (Mathf.FloorToInt(pos.x / chunkSize) + (gridSize / 2)) + "," + (Mathf.FloorToInt(pos.y / chunkSize) + (gridSize / 2));
        chunk.transform.parent = transform;
        chunk.tag = "Group";
        //chunk.transform.position = new Vector3(toWorldValue(Mathf.FloorToInt(pos.x / chunkSize) + (gridSize / 2)), toWorldValue(Mathf.FloorToInt(pos.x / chunkSize) + (gridSize / 2)), 0);
        return chunk.transform;
    }

    void GenerateChunk(Coord chunkPos)
    {
        if (chunksDrawn[(int)chunkPos.x, (int)chunkPos.y]==false) {
            GameObject chunk = new GameObject();
            GetComponent<asteroidControl>().GenerateRandomField((chunkPos.x - (gridSize / 2)) * chunkSize, (chunkPos.y - (gridSize / 2)) * chunkSize, ((chunkPos.x - (gridSize / 2)) * chunkSize) + chunkSize, ((chunkPos.y - (gridSize / 2)) * chunkSize) + chunkSize, 75,chunk);
            chunk.name = "Chunk :" + chunkPos.x + "," + chunkPos.y;
            chunk.transform.parent = transform;
            //chunk.transform.position = new Vector3(toWorldValue(chunkPos.x), toWorldValue(chunkPos.y), 0);
            chunk.tag = "Group";
            chunksDrawn[(int)chunkPos.x, (int)chunkPos.y] = true;
        } else {
            Debug.LogError("Trying to generate already generated chunk!");
        }
    }

    /*void EnableRecursive(Transform group)
    {
        for(int i=0; i<group.childCount;i++)
        {
            Transform current = group.GetChild(i);
            if (current.tag=="Group")
            {
                EnableRecursive(current);
            } else
            {
                if (Vector3.Distance(Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 1)), current.position)<24)
                {
                    current.gameObject.SetActive(true);
                } else
                {
                    //Debug.Log("Deactivate");
                    current.gameObject.SetActive(false);
                }
            }
        }
    }
    */
}
