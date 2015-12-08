using UnityEngine;
using System.Collections;

public class chunkDrawing : MonoBehaviour {
    public int gridSize=10;
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
        currentChunk.x = Mathf.FloorToInt(playerShip.transform.position.x / 100) + (gridSize / 2);
        currentChunk.y = Mathf.FloorToInt(playerShip.transform.position.y / 100) + (gridSize / 2);
        GenerateChunksNine(currentChunk);

    }
	
	// Update is called once per frame
	void Update () {
        currentChunk.x = Mathf.FloorToInt(playerShip.transform.position.x/100) + (gridSize/2);
        currentChunk.y = Mathf.FloorToInt(playerShip.transform.position.y/100) + (gridSize/2);
    }

    void disableChunk(Coord chunkPos)
    {
        if (transform.FindChild("Chunk :" + chunkPos.x + "," + chunkPos.y)!=null) {
            Transform chunkObj = transform.FindChild("Chunk :" + chunkPos.x + "," + chunkPos.y) ;
            for (int i = 0; i< chunkObj.childCount;i++)
            {
                chunkObj.GetChild(i).gameObject.SetActive(false);
            }
        } else
        {
            Debug.LogError("Trying to disable non-existant chunk!");
        }
    }

    void enableChunk(Coord chunkPos)
    {
        if (transform.FindChild("Chunk :" + chunkPos.x + "," + chunkPos.y) != null)
        {
            Transform chunkObj = transform.FindChild("Chunk :" + chunkPos.x + "," + chunkPos.y);
            for (int i = 0; i < chunkObj.childCount; i++)
            {
                chunkObj.GetChild(i).gameObject.SetActive(true);
            }
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

    void GenerateChunk(Coord chunkPos)
    {
        if (chunksDrawn[(int)chunkPos.x, (int)chunkPos.y]==false) { 
        GameObject chunk = GetComponent<asteroidControl>().GenerateRandomField((chunkPos.x - (gridSize / 2)) * 100, (chunkPos.y - (gridSize / 2)) * 100, ((chunkPos.x - (gridSize / 2)) * 100) + 100, ((chunkPos.y - (gridSize / 2)) * 100) + 100, 300);
        chunk.name = "Chunk :" + chunkPos.x + "," + chunkPos.y;
        chunk.transform.parent = transform;
            chunksDrawn[(int)chunkPos.x, (int)chunkPos.y] = true;
            } else
        {
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
