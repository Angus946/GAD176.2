using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class alarmManager : MonoBehaviour 
{
    // listing the unity events to reference in other scripts
    public  UnityEvent alarmActive;
    public void Start()
    {
        GameObject.Find("PlayerArmature").GetComponent<Rigidbody>();
        Rigidbody rb = GetComponent<Rigidbody>();
    }
    // bool to reference for turning alarm
    public bool playerSeen;

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
            }
        }
    }
    // function for test purposes
    public void alarmActiveFunction()
    {
        Debug.Log("alarm is active");
    }
}
