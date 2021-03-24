using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour
{

    //public Transform dest;
    public Transform playerTransform;
    public float rangeOfSight;

    NavMeshAgent navMeshAgent;
    public Animator anim;

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
        if(navMeshAgent == null)
        {
            Debug.LogError("No NavMesh Agent component attached");
        }

        anim = GetComponent<Animator>();
        if (anim == null)
        {
            Debug.LogError("Could not find animator component for enemy.");
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

                if(Vector3.Distance(this.transform.position, playerTransform.transform.position) <= rangeOfSight)
                {
                    currentState = EnemyState.CHASE;
                }
                break;
            
            case EnemyState.CHASE:
                Chasing();
                break;
        }
        /*
        if(dest != null)
        {
            Vector3 target = dest.transform.position;
            navMeshAgent.SetDestination(target);
        }
        */

        //Animate movement
        anim.SetFloat("MovementY", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
    }

    void OnCollisionEnter(Collision other)
    {
        if(other.transform.tag == "Player")
        {
            Debug.Log("Enemy hit the player");
        }
    }

    public void Patrolling()
    {
        if(patrolPoints.Length > 0)
        {
            if (navMeshAgent.remainingDistance <= 0.5 && !navMeshAgent.pathPending)
            {
                navMeshAgent.SetDestination(patrolPoints[currPoint].transform.position);
                currPoint++;

                if(currPoint >= patrolPoints.Length)
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
        }
        else
        {
            Debug.Log("Player transform not set");
        }
    }

    public void Die()
    {
        //TODO: Kill the enemy

        //Disable AI
        
        //Death Animation

        //Either disable the GO after the animation or enable ragdoll physics
        //(can set up animation event to do this)
    }
}
