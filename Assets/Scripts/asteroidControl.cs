using UnityEngine;
using System.Collections;

public class asteroidControl : MonoBehaviour {

    public string[] asteroidObjects;
    public float[] asteroidRarities;
    //public int asteroidAmount;
    //public float maxX;
    //public float maxY;
    //public float minX;
    //public float minY;
    //GameObject asteroidParent;


	// Use this for initialization
	void Start () {

        //GenerateRandomField(maxX,maxY,minX,minY);
	
	}

    public void GenerateRandomField(float mmaxX, float mmaxY, float mminX, float mminY,int amount,GameObject par)
    {
        for (int i = 0; i < amount; i++)
        {
            GameObject currentAsteroid = Instantiate(Resources.Load<GameObject>(GenerateRandomAsteroid()));
            currentAsteroid.transform.parent = par.transform;
            currentAsteroid.transform.position = new Vector3(Random.Range(mmaxX, mminX), Random.Range(mmaxY, mminY), 0);
            currentAsteroid.GetComponent<Rigidbody2D>().angularVelocity = Random.value * 100;
            currentAsteroid.GetComponent<Rigidbody2D>().velocity = Vector2.right * ((Random.value * 2) - 1);
        }
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
        Debug.Log("Error");
        return "Error";
    }
}
