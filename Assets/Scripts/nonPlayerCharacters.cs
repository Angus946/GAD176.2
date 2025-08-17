using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

namespace angusNameSpace
{
    public class nonPlayerCharacters : MonoBehaviour
    {
        // allowing for references to alarm Manager
        public alarmManager alarmManager;
        // bools for player states
        // player seen by the enemys and or cameras
        public bool playerSeen;
        // player in range
        public bool playerInRange;

        // defining a transform for the player transform to be added on to
        public GameObject player;
        // defining a transform for the player transform to be added on to
        public Transform playerTransform;

        // defining the navmesh agent
        public NavMeshAgent agent;
        // creating layermasks to allow custom layers to be referenced in the script
        public LayerMask whatIsGround, whatIsPlayer;


        //default patrolling location
        public Vector3 patrollLocation;
        // bool to say whether the enemy is patrolling
        bool patrolling;
        // folat to set the max range for the patroling enemy
        public float patrolRange;

        // float for attack delay unused for now
        public float attackDelay;
        // bool to stop the enemy from attacking for the delay period
        public bool hasAttacked;

        // floats for the enemy sight and range, which are adjustable in editor
        public float sightRange, attackRange;
        
        public void Start()
        {
            // grabing the player's gameobject from the hierachy, using its name
            player = GameObject.Find("PlayerArmature");
            // grabing own rigid body
            Rigidbody rb = GetComponent<Rigidbody>();
            // grabing its own navmesh agent
            agent = GetComponent<NavMeshAgent>();
        }
        public void Update()
        {
            // calling the is player seen function
            isPlayerSeen();

            // Bool to check if player is in range using the enemy as centre point for sphere cast, attack range as radius, and checking for "what is player" layer
            playerInRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            // if else statements
            // if the player is not seen or in range call the patrol funtion
            if (!playerSeen && !playerInRange)
            {
                // calling the patrolling function
                patrollingFunc();
            }
            // if the player is seen but not in range call chase function
            if (playerSeen && !playerInRange)
            {
                // calling the chase function
                chaseFunc();
            }
            // if the player is seen and in range call the attack function
            if (playerSeen && playerInRange)
            {
                attackFunc();
            }
        }
        // virtual function to allow the status of the player seen bool to be overidden
        public void isPlayerSeen()
        {
            // sphere cast using the enemy's current position as centre point, the sight range fload for the radius, and checking if the what is player tag exists
            if( Physics.CheckSphere(transform.position, sightRange, whatIsPlayer))
            {
                 playerSeen = true;
            }
            else if (alarmManager != null)
            {
                if  (alarmManager.alarmActive == true)
                {
                    playerSeen = true ;
                }
            }
            else
            {
                playerSeen = false;
            }
        }
        // patrolling function doesn't need to return anything
        void patrollingFunc()
        {
            // testing if the enemy is not patrolling, if they are not call the search patrol point function
            if (!patrolling)
            {
                searchPatrolpoint();
            }
            // if the enemy is patrolling use the navMesh agent to move to the patrol location
            if (patrolling)
            {
                // setting the destination for the navmesh to move the enemy to
                agent.SetDestination(patrollLocation);
            }
            // getting the distance of the enemy position from the patrol position
            Vector3 distanceToPatrolPoint = transform.position - patrollLocation;
            // if statement causing the enemy to find a new point to patrol if it is less than 1 unit away from its current patrol point
            if (distanceToPatrolPoint.magnitude < 1f)
            {
                // setting patrolling bool to false causing the first if statement to call the search patrol point function
                patrolling = false;
            }
        }
        // function defining search patrol function
        void searchPatrolpoint()
        {
            // creating a randomised float for the patrol point Z value within the patrol range
            float randomPointZ = Random.Range(-patrolRange, patrolRange); 
            // creating a randomised float for the patrol point X value within the patrol range
            float randomPointX = Random.Range(-patrolRange, patrolRange);
            // setting the patrol point as a new vector 3 point by adding the random X and random Y position
            patrollLocation = new Vector3(transform.position.x + randomPointX, transform.position.y, transform.position.z + randomPointZ);

            // checking if the patrol location is on valid ground
            if (Physics.Raycast(patrollLocation, -transform.up, 2f, whatIsGround))
            {
                // if the ground is valid allow the enemy to go into patrol state
                patrolling = true; 
            }
        }
        // chase function
        void chaseFunc()
        {
            // setting the destination to the player for the navmesh agent
            agent.SetDestination(player.transform.position);
        }
        // attack function
        void attackFunc()
        {
            // setting the destination to itself before attacking 
            agent.SetDestination(transform.position);
            // rotating the enemy to look at the player
            transform.LookAt(player.transform.position);
            // testing the enemy has not yet attacks
            if (!hasAttacked)
            {
                // debug log showing the enemy attacked
                Debug.Log("enemy attacked");
                // setting the has attacked bool
                hasAttacked = true;
                // invoking the attack reset function after a delay
                Invoke(nameof(attackReset), attackDelay);
            }
        }
        // funtion to reset attack
        private void attackReset()
        {
            // setting the has attacked bool to false allowing the enemy to attack again
            hasAttacked = false;
        }
        // alarm active function
        public void alarmActive()
        {
            playerTransform = alarmManager.lastPositionSeen;
            if (playerTransform != null)
            {
                if (alarmManager.alarmActive)
                {
                    playerSeen = alarmManager.alarmActive;
                }
            }
        }
    }
}
