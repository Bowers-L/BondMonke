﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour
{

    //public Transform dest;
    public Transform playerTransform;
    public float rangeOfSight;
    int lightAttackDamage = 4;
    NavMeshAgent navMeshAgent;
    public Animator anim;
    public EnemyStats stats;
    private CombatAgent combat;
    public Vector3 originPoint;
    public bool reset;

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

    private DamageCollider fist;
    private HurtBoxMarker hurtBox;
    private void Awake()
    {
        combat = GetComponent<CombatAgent>();
        if (combat == null)
        {
            Debug.LogError("Player is missing CombatAgent component");
        }
    }
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
            Debug.LogError("Enemy does not have Animator component.");
        }

        stats = GetComponent<EnemyStats>();
        if (stats == null)
        {
            Debug.LogError("Enemy does not have EnemyStats component.");
        }

        fist = GetComponentInChildren<DamageCollider>();
        hurtBox = GetComponentInChildren<HurtBoxMarker>();

        currentState = EnemyState.PATROL;
        currPoint = 0;
        originPoint = this.transform.position;
        reset = false;
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
                if (reset)
                {
                    navMeshAgent.SetDestination(originPoint);
                    currentState = EnemyState.PATROL;
                    reset = false;
                }
                break;
        }
        /*
        if(dest != null)
        {
            Vector3 target = dest.transform.position;
            navMeshAgent.SetDestination(target);
        }
        */
        if (Input.GetKeyUp(KeyCode.K))
        {
            anim.SetTrigger("LightAttack");
            combat.SetDamage(fist, lightAttackDamage);
        }
        if (stats.current_health <= 0)
        {
            Die();
        }

        //Animate movement
        anim.SetFloat("MovementY", navMeshAgent.velocity.magnitude / navMeshAgent.speed);

        //Render the visible hurtbox for debug purposes.
        fist.GetComponent<MeshRenderer>().enabled = GameManager.Instance.debugMode;
        hurtBox.GetComponent<MeshRenderer>().enabled = GameManager.Instance.debugMode;
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
           // Debug.Log("No waypoints set");
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
        enabled = false;

        //Death Animation
        anim.SetTrigger("Death");

        //Either disable the GO after the animation or enable ragdoll physics
        //(can set up animation event to do this)
    }
    public void EnableFistCollider()
    {
        combat.StartAttack(fist);
    }

    public void DisableFistCollider()
    {
        combat.FinishAttack();
    }
}
