using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponManager : MonoBehaviour
{
    [Header("Weapon Variables")]
    //How fast does the weapon fire?
    public float shootingSpeed = 0f;
    //How fast are the weapons projectiles?
    public float projectileSpeed = 0f;
    //How big are the weapon projectiles?
    public float projectileSize = 0f;
    //How much damage do the weapon projectiles do?
    public float projectileDamage = 0f;

    public GameObject bullet;
    private Rigidbody rb;
    //Reference to alarmManger
    public alarmManager alarmManager;

    public virtual void establishStats()
        {
            //Set the stats for the prefab
            shootingSpeed = 1f;
            projectileSpeed = 3f;
            projectileSize = 1f;
            projectileDamage = 1;

        }

    void Start()
    {
        //Overide put in start function
        establishStats();//dada
    }


    void Update()
    {
        if (alarmManager != null && alarmManager.alarmActive == true)
        {
            //Start Blastin'
            shootWeapon();
        }
        else
        {
            Debug.LogWarning("No alarmManager found! Go fix it! Null Error!");
        }

    }

    public void shootWeapon()
    {
        //Time to shoot timer
        shootingSpeed -= Time.deltaTime;
        if (shootingSpeed <= 0f && alarmManager.alarmActive == true)
        {
            //Create a new bullet
            GameObject currentBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            //Make it a rigid body and apply force
            currentBullet.GetComponent<Rigidbody>().AddForce(Vector3.forward.normalized * projectileSpeed, ForceMode.Impulse);
            //Change the size
            transform.localScale *= projectileSize;
            shootingSpeed = 1f;
        }
    }
}
