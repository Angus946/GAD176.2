using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class alarmManager : MonoBehaviour
{
    // listing the unity events to reference in other scripts
    public UnityEvent alarmActive;

    // setting a private transform to reference to avoid self referencial code
    private Transform playerTransform;
    public void Start()
    {
        // grabbing the Transform component from the Player armature
        // Only works if root of player character is named "PlayerArmature"
        // "PlayerArmature" breaks our code convention of only camelCase, due to coming from an external resource
        playerTransform = GameObject.Find("PlayerArmature").GetComponent<Transform>();
    }

    // bool to reference for turning alarm
    public bool playerSeen;

    // Geting/seting the player transform for the players last known position
    public Transform lastPositionSeen
    {
        // This is the getter, it is specifying that we want to return platerTransform to lastPositionSeen
        get
        { return playerTransform; }
        // This is the setter setting the playerTransform value to be specificaly the transform component
        set
        {
            if (playerTransform != null)
            {
                // only setting the transform value if the player is seen
                if (playerSeen)
                {
                    // This is setting the public value to be a private (read only) 
                    // This is done because otherwise the code is a self referential loop
                    playerTransform = transform;
                }
                else
                {
                    // this sets the player transform to null 
                    // this is done as a redundency to stop the enemies from constantly knowing the player location
                    playerTransform = null;
                }
            }
        }
    }

    // built in event active system
    public void OnEnable()
    {
        // null reference check
        if (alarmActive != null)
        {
            // This is adding the Alarm active function as a listener
            //may need to be reworked to automatically add new listeners
            alarmActive.AddListener(alarmActiveFunction);
        }
    }

    // built in event disable system
    public void OnDisable()
    {
        // null reference check
        if (alarmActive != null)
        {
            // removes the alarm activee funtion as a listener
            alarmActive.RemoveListener(alarmActiveFunction);
            // set the Pos variable to be the vector position of the the last place the Character was seen
            Vector3 pos = lastPositionSeen.position;
            // deactivates the player seen bool state
            playerSeen = false;

        }
    }

    private void Update()
    {
        // checking the player seen bool to active alarm
        if (playerSeen)
        {
            // null reference check for the alarm active event
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
        // debug log in a function that gets called when the alarm is active
        Debug.Log("alarm is active");
    }
}
