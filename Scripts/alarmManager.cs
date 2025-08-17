using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alarmManager : MonoBehaviour 
{
    //HHB Wrote This Script

    public bool alarmActive = false;
    public Transform lastPositionSeen;
    public AudioSource alarmSound;
    public bool alarmSoundActive;
    public float alarmTimer = 0f;
    public float alarmTimerMax = 0f;

    void Start()
    {
    alarmSound = GetComponent<AudioSource>();

    //If the alarm is on, loop the sound.
        if (alarmSound != null)
        {
            alarmSound.loop = true;
        }
        else
        {
        Debug.LogWarning("No AudioSource found! Go fix it! Null Error!");
        }
    }

    void Update()
    {
        //Alarm is Active
        if (alarmActive == true)
        {
            alarmTimer += Time.deltaTime;
            if (alarmSoundActive == false)
            {
                alarmSound.Play();
                alarmSoundActive = true;
            }
        }

        //Alarm is Inactive
        else
        {
            alarmSound.Stop();
            alarmSoundActive = false;
        }

        //Reset the Alarm after Max Exceeded and Disable Alarm
        if (alarmTimer >= alarmTimerMax && alarmActive == true)
        {
            alarmActive = false;
            alarmTimer = 0f;
        }   
    }
}
