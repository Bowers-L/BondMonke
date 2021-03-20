using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ProjectileEnemy : MonoBehaviour
{
    public float stopDistance;
    public float retreatDistance;

    public Transform playerTransform;
    NavMeshAgent navMeshAgent;

    public enum EnemyState
    {
        PATROL,
        CHASE
    };
    //Current state of this enemy
    private EnemyState currentState;

    //Array of patrol points
    public GameObject[] patrolPoints;
    private int currPoint;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
        {
            Debug.Log("No NavMesh Agent component attached");
        }

        currentState = EnemyState.PATROL;
        currPoint = 0;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case EnemyState.PATROL:
                Patrolling();

                if (Vector3.Distance(this.transform.position, playerTransform.transform.position) <= 6.0f)
                {
                    currentState = EnemyState.CHASE;
                }
                break;

            case EnemyState.CHASE:
                Chasing();
                break;
        }
    }

    public void Patrolling()
    {
        if (patrolPoints.Length > 0)
        {
            if (navMeshAgent.remainingDistance <= 0.5 && !navMeshAgent.pathPending)
            {
                navMeshAgent.SetDestination(patrolPoints[currPoint].transform.position);
                currPoint++;

                if (currPoint >= patrolPoints.Length)
                {
                    currPoint = 0;
                }
            }
        }
        else
        {
            Debug.Log("No waypoints set");
        }
    }

    public void Chasing()
    {
        if (playerTransform != null)
        {
            //Lookahead prediction
            float dist = (playerTransform.transform.position - this.transform.position).magnitude;
            float lookAheadT = dist / navMeshAgent.speed;
            Vector3 target = playerTransform.transform.position + (lookAheadT * playerTransform.GetComponent<Rigidbody>().velocity);
            navMeshAgent.SetDestination(target);

            if(Vector3.Distance(playerTransform.transform.position, this.transform.position) > stopDistance)
            {
                navMeshAgent.SetDestination(target);
            }
            else if(Vector3.Distance(playerTransform.transform.position, this.transform.position) < stopDistance)//enemy stops at a distance from player instead of running into them
            {
                navMeshAgent.SetDestination(this.transform.position);
            }
        }
        else
        {
            Debug.Log("Player transform not set");
        }
    }
}
