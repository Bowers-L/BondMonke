using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour
{

    [System.Serializable]
    public struct EnemyAttack
    {
        public string attackName;
        public int attackDamage;
    }

    //public Transform dest;
    public Transform playerTransform;
    public float rangeOfSight;
    
    public float attackRange;
    public float attackRestTime;//time between executing an attack
    private float restTimer;

    [SerializeField]
    public EnemyAttack[] enemyAttacks;

    int lightAttackDamage = 4;
    NavMeshAgent navMeshAgent;
    public Animator anim;
    public EnemyStats stats;
    public float combatStoppingDistance = 2f;
    private CombatAgent combat;
    public Vector3 originPoint;
    public bool reset;

    public enum EnemyState
    {
        PATROL,
        CHASE,
        ATTACKING
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

        if (playerTransform == null)
        {
            playerTransform = GameObject.Find("Player").transform;
            if (playerTransform == null)
            {
                Debug.LogError("No player in the scene");
            }
        }

        DeathFader fader = GetComponentInChildren<DeathFader>();
        fader.enabled = false;  //start with the enemy
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


        restTimer = 0;

        originPoint = this.transform.position;
        reset = false;

        //Set a default patrol point if there are none
        if (patrolPoints == null || patrolPoints.Length <= 0)
        {
            Debug.Log("Creating patrol point");
            patrolPoints = new GameObject[1];
            GameObject emptyToSpawn = new GameObject("waypoint");
            patrolPoints[0] = GameObject.Instantiate(emptyToSpawn, transform.position, transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (reset) 
        {
            navMeshAgent.SetDestination(originPoint);
            currentState = EnemyState.PATROL;
            reset = false;
        }
        switch (currentState)
        {
            case EnemyState.PATROL:
                Patrolling();


                if(Vector3.Distance(this.transform.position, playerTransform.transform.position) <= rangeOfSight)
                {
                    this.navMeshAgent.stoppingDistance = combatStoppingDistance;
                    currentState = EnemyState.CHASE;
                }
                break;
            
            case EnemyState.CHASE:
                Chasing();

                if (Vector3.Distance(this.transform.position, playerTransform.transform.position) <= attackRange)
                {
                    currentState = EnemyState.ATTACKING;
                }
                break;

            case EnemyState.ATTACKING:
                Attacking();

                if (Vector3.Distance(this.transform.position, playerTransform.transform.position) > attackRange)
                {
                    currentState = EnemyState.CHASE;
                }

                restTimer -= Time.deltaTime;
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
        

        if (stats.current_health <= 0)
        {
            Die();
        }

        //Animate movement
        anim.SetFloat("MovementY", navMeshAgent.velocity.magnitude / navMeshAgent.speed);

        //Render the visible hurtbox for debug purposes.
        fist.GetComponent<MeshRenderer>().enabled = GameManager.Instance.debugMode;
        hurtBox.GetComponent<MeshRenderer>().enabled = GameManager.Instance.debugMode;

	/* Debug enemy ability to punch
        if (Input.GetKeyUp(KeyCode.K))
        {
            Debug.Log("Enemy Punched");
            anim.SetTrigger("LightAttack");
        }
	*/
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

    public void Attacking()
    {
        //Enemy might be close enough to the player but not facing the player
        if (!isFacingPlayer())
        {
            Debug.Log("Not facing the player!");

            transform.rotation = Quaternion.Lerp(transform.rotation, 
                                                Quaternion.LookRotation(playerTransform.position - transform.position, Vector3.up), 
                                                navMeshAgent.angularSpeed * Time.deltaTime);
            /* Old way of setting the rotation
            transform.rotation = Quaternion.RotateTowards(transform.rotation, 
                                                        Quaternion.Euler(playerTransform.rotation.eulerAngles.x, playerTransform.rotation.eulerAngles.y, playerTransform.rotation.eulerAngles.z), 
                                                        navMeshAgent.angularSpeed * Time.deltaTime);
            */

            Debug.Log("Rotation of enemy: " + transform.rotation);
        }

        if (restTimer <= 0)
        {

            int randomAttack = Random.Range(0, enemyAttacks.Length);
            anim.SetTrigger(enemyAttacks[randomAttack].attackName);
            combat.SetHitboxDamage(fist, enemyAttacks[randomAttack].attackDamage); //call this as animation event
            restTimer = attackRestTime;

        }
    }

    public void Die()
    {
        //TODO: Kill the enemy

        //Disable AI
        enabled = false;
        combat.enabled = false; //So player knows the enemy is dead.
        EventManager.TriggerEvent<DeathAudioEvent, Vector3>(transform.position);
        if (GetComponentInChildren<DeathFader>() == null)
        {
            Debug.Log("DeathFader not added to enemy mesh");
        }
        else
        {
            GetComponentInChildren<DeathFader>().enabled = true;
        }

        //Death Animation
        anim.SetTrigger("Death");

        //Either disable the GO after the animation or enable ragdoll physics
        //(can set up animation event to do this)
    }

    #region Animation Events
    public void OnAttackStart(AttackInfo info)
    {
        combat.SetHitboxDamage(fist, info.damage); //call this as animation event
        combat.EnableHitbox(fist);
        //stats.StaminaCost(info.staminaCost);
    }

    public void OnAttackFinish()
    {
        combat.DisableHitbox();
    }
    #endregion

    private bool isFacingPlayer()
    {
        LayerMask mask = LayerMask.GetMask("Player");
        return Physics.Raycast(transform.position, transform.forward, Mathf.Infinity, mask);
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(new Ray(transform.position, transform.forward));
    }
}
