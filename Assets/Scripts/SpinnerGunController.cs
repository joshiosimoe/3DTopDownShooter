using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinnerGunController : MonoBehaviour
{

    public BulletController bullet;
    //public BulletController explosiveBullet;
    public float bulletSpeed;

    public float timeBetweenShots;
    private float shotCounter;

    public Transform firePoint;
    //public static bool explosiveBulletUnlocked;

    // Start is called before the first frame update
    void Start()
    {
        //explosiveBulletUnlocked = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Controls firing rate
        shotCounter -= Time.deltaTime;
        
        // Creates new bullet
        if(shotCounter <= 0){
            shotCounter = timeBetweenShots;
            BulletController newBullet = Instantiate(bullet, firePoint.position, firePoint.rotation) as BulletController;
            newBullet.speed = bulletSpeed;
        } 
    }
}
