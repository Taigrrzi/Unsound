using UnityEngine;
using System.Collections;

public class GunBrain : MonoBehaviour {

    public bool powered;
    public float mass;
    public shipComputer shipcomputer;
    public int shootKey ;
    public int clockKey;
    public int antKey;
    public int shotConsumption;
    public int ammoType;
    public string bulletResource;
    public int shotHeat;
    public float heatDecay;
    public float currentHeat;
    public int overheatDuration;
    public int gunAngle;
    public float bulletVelocity;
    public GameObject barrel;
    public int shotDelay;
    public float recoil;
    int shotTimer=0;
    bool canShoot;
    public bool overheated;
    public bool ableToShoot;
    int overheatTimer = 0;

    // Use this for initialization
    void Start () {
        ableToShoot = true;
        shipcomputer = transform.parent.GetComponent<shipComputer>();
        overheated = false;
        currentHeat = 0;
        barrel = transform.GetChild(0).gameObject;
        powered = true;
        transform.parent.GetComponent<Rigidbody2D>().mass += mass;
    }
	
	// Update is called once per frame
    void Update()
    {
        if (ableToShoot)
        {
            if (shotTimer < shotDelay)
            {
                shotTimer++;
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
                currentHeat -= heatDecay;
                if (currentHeat<0)
                {
                    currentHeat = 0;
            }
        }
        if (overheated)
        {
            overheatTimer++;
            ableToShoot = false;
            if (overheatTimer>overheatDuration)
            {
                overheated = false;
                ableToShoot = true;
                overheatTimer = 0;
                currentHeat = 50;
            }
        }
    }

	void FixedUpdate () {
        if (shipcomputer.keyBindingStates[shootKey])
        {
            if (powered)
            {
                if (canShoot)
                {
                    if (ammoType == -1)
                    {
                        Shoot();
                    }
                    else if (shipcomputer.ammoAmounts[ammoType] > shotConsumption)
                    {
                        shipcomputer.ammoAmounts[ammoType] -= shotConsumption;
                        Shoot();
                    }
                }
            }
        }
        if (shipcomputer.keyBindingStates[clockKey])
        {
            gunAngle+=5;
        }
        else if (shipcomputer.keyBindingStates[antKey])
        {
            gunAngle-=5;
        }
        gunAngle = Mathf.Clamp(gunAngle,-60,60);
        barrel.transform.localEulerAngles = new Vector3(0, 0, gunAngle);
    }

    public void Shoot()
    {
        GameObject prefab = Resources.Load<GameObject>(bulletResource);
        GameObject bullet = (GameObject)Instantiate(prefab, barrel.transform.GetChild(0).position, Quaternion.identity);
        bullet.GetComponent<Rigidbody2D>().rotation = barrel.transform.rotation.z;
        //Debug.Log(barrel.transform.rotation.eulerAngles);
        bullet.GetComponent<Rigidbody2D>().velocity = GetComponentInParent<Rigidbody2D>().velocity;
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(),barrel.GetComponent<Collider2D>());
        Physics2D.IgnoreCollision(bullet.GetComponent<Collider2D>(),GetComponent<Collider2D>());
        bullet.GetComponent<Rigidbody2D>().AddForce(barrel.transform.right * bulletVelocity,ForceMode2D.Impulse);
        GetComponentInParent<Rigidbody2D>().AddForce(barrel.transform.right *(-recoil),ForceMode2D.Impulse);
        canShoot = false;
        shotTimer = 0;
        currentHeat += shotHeat;
    }
}
