using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class alarmManager : MonoBehaviour 
{
    // listing the unity events to reference in other scripts
    public static UnityEvent alarmActive;

    private Transform playerTransform;
    public void Start()
    {
        playerTransform = GameObject.Find("PlayerArmature").GetComponent<Transform>();
    }

    // bool to reference for turning alarm
    public bool playerSeen;

    // Geting/seting the player transform for the players last known position
    public Transform lastPositionSeen   
    { get 
        { return playerTransform; } 
        set 
        { 
            if (playerTransform != null)
            {
                // only setting the transform value if the player is seen
                if (playerSeen)
                {
                    playerTransform = transform;
                }
                
            }
        }
    } 

    // built in event active system
    public void OnEnable()
    {
        if (alarmActive != null)
        {
            alarmActive.AddListener(alarmActiveFunction);
        }
    }

    // built in event disable system
    public void OnDisable()
    {
        if(alarmActive !=null)
        {
            alarmActive.RemoveListener(alarmActiveFunction);
            Vector3 pos = lastPositionSeen.position;
            playerSeen = false;
            
        }
    }

    private void Update()
    {
        // checking the player seen bool to active alarm
        if (playerSeen)
        {
            if (alarmActive != null)
            {
                // invoking the unity event
                alarmActive?.Invoke();
                // debug log showing the player position, when alarm is active
                if (playerTransform != null)
                Debug.Log(playerTransform.position);
            }
        }
    }
    // function for test purposes
    public void alarmActiveFunction()
    {
        Debug.Log("alarm is active");
    }
}
