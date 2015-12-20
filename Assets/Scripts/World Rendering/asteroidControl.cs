using UnityEngine;
using System.Collections;

public class asteroidControl : MonoBehaviour {

    public string[] asteroidObjects;
    public float[] asteroidRarities;


	// Use this for initialization
	void Start () {

        //GenerateRandomField(maxX,maxY,minX,minY);
	
	}

    public void GenerateRandomField(float mmaxX, float mmaxY, float mminX, float mminY,int amount,GameObject par)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject prefab = Resources.Load<GameObject>(GenerateRandomAsteroid());
            prefab.GetComponent<debrisBrain>().chunkDrawing = transform;
            GameObject currentAsteroid = Instantiate(prefab);
            currentAsteroid.transform.parent = par.transform;
            currentAsteroid.transform.position = new Vector3(Random.Range(mmaxX, mminX), Random.Range(mmaxY, mminY), 0);
            currentAsteroid.GetComponent<Rigidbody2D>().angularVelocity = Random.value * 300;
            currentAsteroid.GetComponent<Rigidbody2D>().velocity = Vector2.right *((Random.value * 2) - 1)*(Random.value*5);
        }
    }


    public GameObject[] GenerateRandomCircle(float X,float Y, float radius, int amount, GameObject par)
    {
        GameObject[] asteroidArray = new GameObject[amount];
        for (int i = 0; i < amount; i++)
        {
            Vector2 randomPoint = Random.insideUnitCircle * radius;
            GameObject prefab = Resources.Load<GameObject>(GenerateRandomAsteroid());
            prefab.GetComponent<debrisBrain>().chunkDrawing = transform;
            GameObject currentAsteroid = Instantiate(prefab);
            currentAsteroid.transform.parent = par.transform;
            currentAsteroid.transform.position = new Vector3(X + randomPoint.x, Y+randomPoint.y, 0);
            currentAsteroid.GetComponent<Rigidbody2D>().angularVelocity = Random.value * 300;
            currentAsteroid.GetComponent<Rigidbody2D>().velocity = Vector2.right * ((Random.value * 2) - 1) * (Random.value * 5);
            asteroidArray[i] = currentAsteroid;
        }
        return asteroidArray;
    }

    public GameObject GenerateRandomAsteroidOnCircle(float X, float Y, float radius, GameObject par)
    {
        int angle = Mathf.FloorToInt(Random.value * 360);
        Vector2 moveVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * radius;
        GameObject prefab = Resources.Load<GameObject>(GenerateRandomAsteroid());
        prefab.GetComponent<debrisBrain>().chunkDrawing = transform;
        GameObject currentAsteroid = Instantiate(prefab);
        currentAsteroid.transform.parent = par.transform;
        currentAsteroid.transform.position = new Vector3(X+moveVector.x,Y+moveVector.y, 0);
        currentAsteroid.GetComponent<Rigidbody2D>().angularVelocity = Random.value * 300;
        currentAsteroid.GetComponent<Rigidbody2D>().velocity = Vector2.right * ((Random.value * 2) - 1) * (Random.value * 5);
        return currentAsteroid;
    }

    public string GenerateRandomAsteroid()
    {
        float rand = Random.value;
        float currentValue = 0;
        for (int i = 0; i < asteroidObjects.Length; i++)
        {
            if (rand>currentValue&&rand<currentValue+asteroidRarities[i])
            {
                return asteroidObjects[i];
            } else
            {
                currentValue += asteroidRarities[i];
            }
        }
        Debug.Log("Asteroid Distribution Values Add Up To More Than One!");
        return "Error";
    }
}
