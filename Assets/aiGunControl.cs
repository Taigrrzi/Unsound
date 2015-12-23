using UnityEngine;
using System.Collections;

public class aiGunControl : MonoBehaviour {

    public bool powered;
    public string bulletResource;
    public int shotHeat;
    public float heatDecay;
    public float currentHeat;
    public float overheatDuration;
    public int gunAngle;
    public float bulletVelocity;
    public GameObject barrel;
    public float shotDelay;
    public float recoil;
    float shotTimer = 0;
    bool canShoot;
    public bool overheated;
    public bool ableToShoot;
    float overheatTimer = 0;

    // Use this for initialization
    void Start () {
        ableToShoot = true;
        overheated = false;
        currentHeat = 0;
        barrel = transform.GetChild(0).gameObject;
        powered = true;
    }
	
	void Update()
    {
        if (ableToShoot)
        {
            if (shotTimer<shotDelay/Time.deltaTime)
            {
                shotTimer+=1;
            }
            else
            {
                canShoot = true;
            }
        }
        if (currentHeat>100)
        {
            overheated = true;
        } else if (currentHeat>0)
            {
                currentHeat -= heatDecay* Time.deltaTime;
                if (currentHeat<0)
                {
                    currentHeat = 0;
            }
        }
        if (overheated)
        {
            overheatTimer+=1;
            ableToShoot = false;
            if (overheatTimer>overheatDuration/Time.deltaTime)
            {
                overheated = false;
                ableToShoot = true;
                overheatTimer = 0;
                currentHeat = 50;
            }
        }
    }
}
