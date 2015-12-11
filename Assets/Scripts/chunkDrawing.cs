using UnityEngine;
using System.Collections;

public class chunkDrawing : MonoBehaviour {
    /*public int gridSize= 100;
    public int chunkSize = 50;
    public GameObject[,] chunkArray = new GameObject[0, 0];
    public bool[,] chunksEnabledArray = new bool[0, 0];
    public Coord currentChunk;
    public GameObject playerShip;

    // Use this for initialization
    void Start () {
        chunkArray = new GameObject[gridSize, gridSize];
        chunksEnabledArray = new bool[gridSize, gridSize];

        for (int i=0; i< gridSize;i++)
        {
            for (int j=0; j< gridSize; j++)
            {
                chunkArray[i, j] = null;
                chunksEnabledArray[i, j] = false;
            }
        }
        GenerateChunksNine(toChunkValue(playerShip.transform.position.x), toChunkValue(playerShip.transform.position.y));

    }

    // Update is called once per frame
    void Update()
    {
        GenerateMissingNearby(playerShip);

        int playerX = toChunkValue(playerShip.transform.position.x);
        int playerY = toChunkValue(playerShip.transform.position.y);

        for (int i = 0; i < gridSize; i++)
        {
            for (int j = 0; j < gridSize; j++)
            {
                if (chunkArray[i, j] != null)
                {
                    if (Mathf.Sqrt(Mathf.Pow(i - playerX, 2) + Mathf.Pow(j - playerY, 2)) > 1.7f)
                    {
                        disableChunk(i, j);
                    }
                    else
                    {
                        enableChunk(i, j);
                    }
                }
            }
        }
    }

/*    void destroyNonEssential(Transform chunkObj)
    {
        for (int k=0; k<chunkObj.childCount;k++)
        {
            if (chunkObj.GetChild(k).tag=="NonEssential")
            {
                Destroy(chunkObj.GetChild(k).gameObject);
            }
        }
        string[] splitted = chunkObj.name.Split(',');
        // Debug.Log(splitted[0].Substring(7));
        int chunkx = int.Parse(splitted[0].Substring(7));
        int chunky = int.Parse(splitted[1]);
        chunksDrawn[chunkx, chunky] = false;
        Debug.Log("Destroyed Chunk: "+chunkx+","+chunky);

    }*/
    /*
    public GameObject myChunk(Transform targetObj)
    {

        return chunkArray[toChunkValue(targetObj.position.x), toChunkValue(targetObj.position.y)];
    }

    int toChunkValue(float worldvalue)
    {
        return (Mathf.FloorToInt(worldvalue / chunkSize) + (gridSize / 2));
    }

    float toWorldValue(int chunkvalue)
    {
        return ((chunkvalue-(gridSize / 2))*chunkSize);
    }

    void disableChunk(int x, int y)
    {
        GameObject chunkObj = chunkArray[x, y];
        if (chunkObj != null)
        {
            if (chunksEnabledArray[x, y])
            {
                //Debug.Log("Disabled Chunk: " + x + "," + y);
                for (int i = 0; i < chunkObj.transform.childCount; i++)
                {
                    if (chunkObj.transform.GetChild(i).tag == "NonEssential")
                    {
                        chunkObj.transform.GetChild(i).GetComponent<debrisBrain>().SaveVeloc();
                        chunkObj.transform.GetChild(i).GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                        chunkObj.transform.GetChild(i).GetComponent<Collider2D>().enabled = false;
                        chunkObj.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = false;
                        chunkObj.transform.GetChild(i).GetComponent<debrisBrain>().enabled = false;
                    }
                    else
                    {
                        Debug.Log("Essential Object Not Unloaded");
                    }
                }
                chunksEnabledArray[x, y] = false;
            }
        }
        else
        {
            Debug.LogError("Trying to disable non-generated chunk: " + x + "," + y + "!");
        }
    }

    void enableChunk(int x, int y)
    {
        GameObject chunkObj = chunkArray[x, y];
        if (chunkObj != null)
        {
            if (!chunksEnabledArray[x, y])
            {
                //Debug.Log("Re-Enabled Chunk: " + x + "," + y);
                for (int i = 0; i < chunkObj.transform.childCount; i++)
                {
                    if (chunkObj.transform.GetChild(i).tag == "NonEssential")
                    {
                        chunkObj.transform.GetChild(i).GetComponent<Collider2D>().enabled = true;
                        chunkObj.transform.GetChild(i).GetComponent<SpriteRenderer>().enabled = true;
                        chunkObj.transform.GetChild(i).GetComponent<debrisBrain>().enabled = false;
                        chunkObj.transform.GetChild(i).GetComponent<debrisBrain>().LoadVeloc();
                    }
                }
                //chunkObj.gameObject.SetActive(true);
            }
        }
        else
        {
            Debug.LogError("Trying to enable non-generated chunk: " + x + "," + y + "!");
        }
            chunksEnabledArray[x, y] = true;
    }

    public Transform ChunkAt(Transform target)
    {
        if (chunkArray[toChunkValue(target.position.x), toChunkValue(target.position.y)] != null)
        {
            return chunkArray[toChunkValue(target.position.x), toChunkValue(target.position.y)].transform;
        }
        else
        {
            return null;
        }
    }

    void GenerateChunksSurrounding(int x, int y)
    {
        for (int i=0; i<3; i++)
        {
            for (int j=0; j<3; j++)
            {
                if (i!=1||j!=1)
                {
                    GenerateChunk(x+i-1,y+j-1);
                }
            }
        }
    }

    void GenerateChunksNine(int x, int y)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                GenerateChunk(x + i - 1, y + j - 1);
            }
        }
    }

    void GenerateChunksList(int[] xs, int[] ys)
    {
        int xlen = xs.Length;
        if (xlen != ys.Length)
        {
            Debug.LogError("Cannot Create Chunk List with Inequal Amount of Y's and X's");
        } else {
            for (int i = 0; i < xlen; i++)
            {
                GenerateChunk(xs[i],ys[i]);
            }
        }
    }

    void GenerateMissingNearby(int x, int y)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (chunkArray[x + i - 1, y + j - 1]==null)
                {
                    GenerateChunk(x + i - 1,y + j - 1);
                }
            }
        }
    }

    void GenerateMissingNearby(GameObject target)
    {
        GenerateMissingNearby(toChunkValue(target.transform.position.x), toChunkValue(target.transform.position.y));
    }


    /*public Transform GenerateEmpty(Vector3 pos) 
    {
        GameObject chunk = new GameObject();

        chunk.name = "Chunk :" + (Mathf.FloorToInt(pos.x / chunkSize) + (gridSize / 2)) + "," + (Mathf.FloorToInt(pos.y / chunkSize) + (gridSize / 2));
        chunk.transform.parent = transform;
        chunk.tag = "Group";
        //chunk.transform.position = new Vector3(toWorldValue(Mathf.FloorToInt(pos.x / chunkSize) + (gridSize / 2)), toWorldValue(Mathf.FloorToInt(pos.x / chunkSize) + (gridSize / 2)), 0);
        return chunk.transform;
    }*/
    /*
    public Transform GenerateChunk(int x, int y)
    {
        if (chunkArray[x,y]==null) {
            GameObject chunk = new GameObject();
            GetComponent<asteroidControl>().GenerateRandomField((x - (gridSize / 2)) * chunkSize, (y - (gridSize / 2)) * chunkSize, ((x - (gridSize / 2)) * chunkSize) + chunkSize, ((y - (gridSize / 2)) * chunkSize) + chunkSize, 75,chunk);
            chunk.name = "Chunk :" + x + "," + y;
            //Debug.Log("Generated Chunk :" + x + "," + y);
            chunk.transform.parent = transform;
            chunk.tag = "Group";
            chunkArray[x,y] = chunk;
            return chunk.transform;
        } else {
            Debug.LogError("Trying to generate already generated chunk at "+x+","+y+" !");
            return null;
        }
    }

    public Transform GenerateChunk(Transform target)
    {
        int x = toChunkValue(target.position.x);
        int y = toChunkValue(target.position.y);

        if (chunkArray[x, y] == null)
        {
            GameObject chunk = new GameObject();
            GetComponent<asteroidControl>().GenerateRandomField((x - (gridSize / 2)) * chunkSize, (y - (gridSize / 2)) * chunkSize, ((x - (gridSize / 2)) * chunkSize) + chunkSize, ((y - (gridSize / 2)) * chunkSize) + chunkSize, 75, chunk);
            chunk.name = "Chunk :" + x + "," + y;
            chunk.transform.parent = transform;
            chunk.tag = "Group";
            chunkArray[x, y] = chunk;
            return chunk.transform;
        }
        else
        {
            Debug.LogError("Trying to generate already generated chunk!");
            return null;
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
