using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace angusNameSpace
{
    public class nonPlayerCharacters : MonoBehaviour
    {
        // allowing for references to alarm Manager
        public alarmManager alarmManager;
        public UnityEvent alarmActive;

        public void Start()
        {
            
        }
        private void onAlarm()
        {
            if (alarmManager != null)
            {
                alarmActive.AddListener(activeFunction);
            }
        }
        public virtual void baseLogic()
        {

        }
        public void activeFunction()
        {
            Debug.Log("alarm active probably");
        }
    }

    public class guardPatrol : nonPlayerCharacters
    {

    }
}
