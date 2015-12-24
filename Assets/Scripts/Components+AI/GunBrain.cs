using UnityEngine;
using System.Collections;

public class GunBrain : MonoBehaviour {

    public bool powered;
    public shipComputer shipcomputer;
    public KeyCode shootKey ;
    public KeyCode clockKey;
    public KeyCode antKey;
    public int shotConsumption;
    public int ammoType;
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
    float shotTimer=0;
    bool canShoot;
    public bool overheated;
    public bool ableToShoot;
    float overheatTimer = 0;

    // Use this for initialization
    void Start () {
        ableToShoot = true;
        shipcomputer = transform.parent.GetComponent<shipComputer>();
        overheated = false;
        currentHeat = 0;
        barrel = transform.GetChild(0).gameObject;
        powered = true;
    }
	
	// Update is called once per frame
    void Update()
    {
        powered = GetComponent<shipComponentControl>().powered;
        if (powered)
        {
            if (ableToShoot)
            {
                if (shotTimer < shotDelay / Time.deltaTime)
                {
                    shotTimer += 1;
                }
                else
                {
                    canShoot = true;
                }
            }
        }
        if (currentHeat>100)
        {
            overheated = true;
        } else if (currentHeat>0)
            {
                currentHeat -= heatDecay*Time.deltaTime;
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

	void FixedUpdate () {
        if (Input.GetKey(shootKey))
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
        if (Input.GetKey(clockKey))
        {
            gunAngle+=5;
        }
        else if (Input.GetKey(antKey))
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
