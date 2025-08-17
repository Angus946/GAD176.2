using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraHead : MonoBehaviour
{
    [Header("Camera Rotate Variables")]
    public float turnSpeed = 0f;
    public float turnArcMaxTimer = 2f;
    public float turnTimer = 0f;

    [Header("Camera Vision Variables")]
    private float sphereRadius = 1f;
    private float maxDistance = 25f;
    public RaycastHit hit;
    public LayerMask hitLayers; 

    [Header("Camera Bools")]
    public bool swapRotation = false;
    public alarmManager alarmManager;

    void Update()
    {
        cameraDetection();
    
        //This code is atrocious and I am ashamed! - HHB
        //If the bool "swapRotation" is true, turn on the Y axis until the Max Arc is reached. Then untick that bool and flip the rotation.
        if (swapRotation == true)
        {
            transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            turnTimer += Time.deltaTime;
            if (turnTimer >= turnArcMaxTimer)
            {
                turnTimer = 0;
                swapRotation = false;
            }
        }

        if (swapRotation == false)
        {
            transform.Rotate(0, -turnSpeed * Time.deltaTime, 0);
            turnTimer += Time.deltaTime;
            if (turnTimer >= turnArcMaxTimer)
            {
                turnTimer = 0;
                swapRotation = true;
            }
        }
    }
        //Shoot out a raycast sphere and have any player struck by it trigger the alarm
        void cameraDetection()
        {
            if (Physics.SphereCast(transform.position, sphereRadius, transform.forward, out hit, maxDistance, hitLayers))
            {
            Debug.Log("Hit: " + hit.collider.name);
            alarmManager.alarmActive = true;

                // Draw the ray to the hit point
            Debug.DrawRay(transform.position, transform.forward * hit.distance, Color.red, 1f);
            }
        }

}

