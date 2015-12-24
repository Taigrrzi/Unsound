using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DistanceDrawing : MonoBehaviour {

    public float  generateDistance = 2 ;
    public GameObject playerShip;
    public GameObject[] circleArray;
    public List<GameObject> tempCircle;
    asteroidControl astControl;
    public float asteroidDensity;
    int asteroidAmount;

    // Use this for initialization
    void Start () {
        astControl = GetComponent<asteroidControl>();
        asteroidAmount = Mathf.FloorToInt(asteroidDensity*Mathf.PI*generateDistance*generateDistance);
        circleArray = GetComponent<asteroidControl>().GenerateRandomCircle(playerShip.transform.position.x, playerShip.transform.position.y,generateDistance,asteroidAmount,gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        foreach (GameObject target in tempCircle)
        {
            if (target == null) {
                tempCircle.Remove(target);
                break;
            }
            else if (Vector2.Distance(playerShip.transform.position, target.transform.position) > generateDistance)
            {
                Destroy(target);
            }
        }

        asteroidAmount = Mathf.FloorToInt(asteroidDensity * Mathf.PI * generateDistance * generateDistance);
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
