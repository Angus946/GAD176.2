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
        public bool playerSeen;
        public bool playerInRange;

        public GameObject player;
        public Transform playerTransform;

        public Vector3 enemyPosition;
        private float moveSpeed = 10;
        private float moveSpeedMultiplier = 5;
        private float detectionSpeed = 3;

        public NavMeshAgent agent;
        public LayerMask whatIsGround, whatIsPlayer;


        //default patrolling
        public Vector3 patrollLocation;
        bool patrolling;
        public float patrolRange;

        public float attackDelay;
        public bool hasAttacked;

        public float sightRange, attackRange;
        
        public void Start()
        {
            player = GameObject.Find("PlayerArmature");
            Rigidbody rb = GetComponent<Rigidbody>();
            agent = GetComponent<NavMeshAgent>();
        }
        public void Update()
        {
            playerSeen = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
            playerInRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);
            if (!playerSeen && !playerInRange)
            {
                patrollingFunc();
            }
            if (playerSeen && !playerInRange)
            {
                chaseFunc();
            }
            if (playerSeen && playerInRange)
            {
                attackFunc();
            }
        }

        void patrollingFunc()
        {
            if (!patrolling)
            {
                searchPatrolpoint();
            }
            if (patrolling)
            {
                agent.SetDestination(patrollLocation);
            }
            Vector3 distanceToPatrolPoint = transform.position - patrollLocation;
            if (distanceToPatrolPoint.magnitude < 1f)
            {
                patrolling = false;
            }
        }
        void searchPatrolpoint()
        {
            float randomPointZ = Random.Range(-patrolRange, patrolRange); 
            float randomPointX = Random.Range(-patrolRange, patrolRange);
            patrollLocation = new Vector3(transform.position.x + randomPointX, transform.position.y, transform.position.z + randomPointZ);

            if (Physics.Raycast(patrollLocation, -transform.up, 2f, whatIsGround))
            {
                patrolling = true; 
            }
        }
        void chaseFunc()
        {
            agent.SetDestination(player.transform.position);
        }
        void attackFunc()
        {
            agent.SetDestination(transform.position);
            transform.LookAt(player.transform.position);
            if (!hasAttacked)
            {
                Physics.Raycast(Vector3.forward, attackRange)
                hasAttacked = true;
                Invoke(nameof(attackReset), attackDelay);
            }
        }
        private void attackReset()
        {
            hasAttacked = false;
        }
        public void alarmActive()
        {
            playerTransform = alarmManager.lastPositionSeen;
            if (playerTransform != null)
            {
                transform.LookAt(playerTransform);
            }
        }
    }

    public class guardPatrol : nonPlayerCharacters
    {

    }
}
