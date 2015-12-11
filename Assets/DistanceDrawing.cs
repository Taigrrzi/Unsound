using UnityEngine;
using System.Collections;

public class DistanceDrawing : MonoBehaviour {

    public float  generateDistance = 2 ;
    public GameObject playerShip;
    public GameObject[] circleArray;
    asteroidControl astControl;
    public int asteroidAmount;

    // Use this for initialization
    void Start () {
        astControl = GetComponent<asteroidControl>();
        circleArray = GetComponent<asteroidControl>().GenerateRandomCircle(playerShip.transform.position.x, playerShip.transform.position.y,generateDistance,asteroidAmount,gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        for (int i=0; i<circleArray.Length;i++)
        {
           if  (circleArray[i]==null)
            {
                circleArray[i] = astControl.GenerateRandomAsteroidOnCircle(playerShip.transform.position.x, playerShip.transform.position.y, generateDistance, gameObject);
            } else
            {
                if (Vector2.Distance(playerShip.transform.position,circleArray[i].transform.position)>generateDistance)
                {
                    Destroy(circleArray[i]) ;
                    circleArray[i] = astControl.GenerateRandomAsteroidOnCircle(playerShip.transform.position.x, playerShip.transform.position.y, generateDistance, gameObject);
                }
            }
        }
	}
}
