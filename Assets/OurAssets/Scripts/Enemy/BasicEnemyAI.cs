using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BasicEnemyAI : MonoBehaviour
{

    /* DON'T USE THIS ANYMORE
    [System.Serializable]
    public struct EnemyAttack
    {
        public string attackName;
        public int attackDamage;
    }
    */

    public enum EnemyType
    {
        NORMAL,
        BLOCKING
    }

    public EnemyType enemyType;

    //public Transform dest;
    public Transform playerTransform;
    public PlayerInputController playerInput;
    public Animator playerAnim;

    public float rangeOfSight;
    
    public float attackRange;
    public float attackRestTime;//time between executing an attack
    private float restTimer;

    [SerializeField]
    public AttackInfo[] enemyAttacks;

    NavMeshAgent navMeshAgent;
    public Animator anim;
    public EnemyStats stats;
    //public float combatStoppingDistance = 2f;
    private CombatAgent combat;
    public Vector3 originPoint;
    public bool reset;
    public bool blocking = false;
    //private float maxBlockRate = 1;
    public float blockRate = 1.0f;//chance for enemy to block if player attack is read

    public enum EnemyState
    {
        PATROL,
        CHASE,
        ATTACKING
    };

    //Current state of this enemy
    [SerializeField]
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
            Debug.LogError("Enemy is missing CombatAgent component");
        }

        if (playerTransform == null)
        {
            playerTransform = GameObject.Find("Player").transform;
            if (playerTransform == null)
            {
                Debug.LogError("No Player in the scene");
            }
        }

        if (playerInput == null)
        {
            playerInput = playerTransform.gameObject.GetComponent<PlayerInputController>();
            if (playerInput == null)
            {
                Debug.LogError("Player Input Component Not Found");
            }
        }

        if (playerAnim == null)
        {
            playerAnim = playerTransform.gameObject.GetComponent<Animator>();
            if (playerInput == null)
            {
                Debug.LogError("Player Animator Component Not Found");
            }
        }

        navMeshAgent = this.GetComponent<NavMeshAgent>();
        if (navMeshAgent == null)
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

        DeathFader fader = GetComponentInChildren<DeathFader>();
        if (fader == null)
        {
            Debug.LogError("Enemy is missing Fader Component");
        }
        fader.enabled = false;  //start with the enemy
    }
    // Start is called before the first frame update
    void Start()
    {
        //navMeshAgent.updatePosition = false;    //Position should be determined by animator controller via root motion.
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
            //Debug.Log("Creating patrol point");
            patrolPoints = new GameObject[1];
            GameObject emptyToSpawn = new GameObject("waypoint");
            patrolPoints[0] = GameObject.Instantiate(emptyToSpawn, transform.position, transform.rotation);
        }

        GameManager.Instance.controls.Player.LightAttack.performed += ctx => OnPlayerAttemptedAttack();
        GameManager.Instance.controls.Player.HeavyAttack.performed += ctx => OnPlayerAttemptedAttack();
    }

    // Update is called once per frame
    void Update()
    {

        //Animate movement
        anim.SetBool("Block", blocking);
        anim.SetFloat("MovementY", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        anim.SetFloat("MovementMag", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        //combat.isBlocking = anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex("Combat")).IsName("Block");
        combat.isBlocking = blocking && !anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex("Combat")).IsName("BlockBroken");
        combat.isInvincible = anim.GetCurrentAnimatorStateInfo(anim.GetLayerIndex("Combat")).IsTag("Invincible");
        //UpdateMovement();


        if (reset) 
        {
            navMeshAgent.SetDestination(originPoint);
            stats.current_health = stats.max_health;
            currentState = EnemyState.PATROL;
            reset = false;
        }

        switch (currentState)
        {
            case EnemyState.PATROL:
                Patrolling();


                if(Vector3.Distance(this.transform.position, playerTransform.transform.position) <= rangeOfSight)
                {
                    this.navMeshAgent.stoppingDistance = attackRange;
                    currentState = EnemyState.CHASE;
                }
                break;
            
            case EnemyState.CHASE:
                Chasing();

                if (Vector3.Distance(this.transform.position, playerTransform.transform.position) <= attackRange)
                {
                    currentState = EnemyState.ATTACKING;
                }
                if (Vector3.Distance(this.transform.position, playerTransform.transform.position) > rangeOfSight)
                {
                    currentState = EnemyState.PATROL;
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

    
    //Source: https://docs.unity3d.com/Manual/nav-CouplingAnimationAndNavigation.html
    private void UpdateMovement()
    {
        Vector3 worldDeltaPosition = navMeshAgent.nextPosition - transform.position;
        Vector2 velocity = Vector2.zero;
        Vector2 smoothDeltaPosition = Vector2.zero;

        // Map 'worldDeltaPosition' to local space
        float dx = Vector3.Dot(transform.right, worldDeltaPosition);
        float dy = Vector3.Dot(transform.forward, worldDeltaPosition);
        Vector2 deltaPosition = new Vector2(dx, dy);

        // Low-pass filter the deltaMove
        //float smooth = Mathf.Min(1.0f, Time.deltaTime / 0.15f);
        //smoothDeltaPosition = Vector2.Lerp(smoothDeltaPosition, deltaPosition, smooth);

        // Update velocity if time advances
        if (Time.deltaTime > 1e-5f)
            velocity = deltaPosition / Time.deltaTime;
        velocity = deltaPosition;
        bool shouldMove = velocity.magnitude > 0.5f && navMeshAgent.remainingDistance > navMeshAgent.radius;

        // Update animation parameters
        if (shouldMove)
        {
            anim.SetFloat("MovementX", velocity.x);
            anim.SetFloat("MovementY", velocity.y);
            anim.SetFloat("MovementMag", velocity.magnitude);
        } else
        {
            anim.SetFloat("MovementX", 0.0f);
            anim.SetFloat("MovementY", 0.0f);
            anim.SetFloat("MovementMag", 0.0f);
        }


        //Script in example that causes agent to move their head towards a target using IK
        //GetComponent<LookAt>().lookAtTargetPosition = agent.steeringTarget + transform.forward;
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
        if (!playerAnim.GetCurrentAnimatorStateInfo(playerAnim.GetLayerIndex("Combat")).IsTag("Attack"))
        {
            //Get out of the blocking state before attacking
            blocking = false;
            anim.SetBool("Block", blocking);
        }
        //hurtBox.GetComponent<CapsuleCollider>().enabled = !blocking;  //DON'T DO THIS WITH NEW COMBAT SYSTEM!

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

            //Debug.Log("Rotation of enemy: " + transform.rotation);
        }


        //hurtBox.GetComponent<CapsuleCollider>().enabled = !blocking;  //DON'T DO THIS WITH NEW COMBAT SYSTEM!

        if (restTimer <= 0)
        {
            //If player is still in an attacking state, don't unblock
            if (!playerAnim.GetCurrentAnimatorStateInfo(playerAnim.GetLayerIndex("Combat")).IsTag("Attack"))
            {
                //Get out of the blocking state before attacking
                blocking = false;
                anim.SetBool("Block", blocking);
            }

            if (!blocking)
            {
                if (enemyAttacks.Length == 0)
                {
                    Debug.LogWarning("No attacks for this enemy specified in the inspector");
                }
                else
                {
                    int randomAttack = Random.Range(0, enemyAttacks.Length);
                    anim.SetTrigger(enemyAttacks[randomAttack].attackName);
                    combat.SetHitboxDamage(fist, enemyAttacks[randomAttack]);
                    restTimer = attackRestTime;
                }
            }

        }
    }

    public void OnPlayerAttemptedAttack()
    {
        //Blocking type enemies can read player input and attempt to block.
        //If the rest timer is up, the enemy is going to either attempt an attack or continue blocking,
        //so don't let this callback interrupt that.
        if (enemyType == EnemyType.BLOCKING //&& (restTimer > 0 || currentState != EnemyState.ATTACKING)
            )
        {
            //Enemy will block if player attack is read and based on set block rate of enemy
            float blockChance = Random.Range(0f, 1f);
            if (blockChance <= blockRate)
            {
                Debug.Log("Read player attack");
                blocking = true;
                combat.isBlocking = true;
                currentState = (currentState == EnemyState.CHASE) ? EnemyState.ATTACKING : currentState;
            }
            anim.SetBool("Block", blocking);
        }
    }

    public void Respawn()
    {

        stats.current_health = stats.max_health;
        transform.position = originPoint;
        anim.SetTrigger("Reset");
        reset = true;

    }

    public void Die()
    {
        //TODO: Kill the enemy

        CollectableDropper dropper = GetComponent<CollectableDropper>();
        if (dropper != null)
        {
            Debug.Log("Dropped Collectable");
            dropper.DropCollectable();
        }

        //Disable AI
        enabled = false;
        combat.enabled = false; //So player knows the enemy is dead.
        fist.DisableDamageCollider();   //Don't let the player run into dead enemy and die to it.
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
        anim.SetTrigger("LockCombatLayer");

        GameManager.Instance.playtestStats.incEnemiesDefeated();

        //Either disable the GO after the animation or enable ragdoll physics
        //(can set up animation event to do this)

        if (gameObject.name.CompareTo("TutorialEnemy") == 0)
        {
            Animator elevatorAnim = GameObject.Find("elevator").GetComponent<Animator>();
            elevatorAnim.SetBool("TutEnemyDefeated", true);
        }
    }

    #region Animation Events
    public void OnAttackStart(AttackInfo info)
    {
        combat.SetHitboxDamage(fist, info); //call this as animation event
        combat.EnableHitbox(fist);
        //stats.StaminaCost(info.staminaCost);
    }

    public void OnAttackFinish()
    {
        combat.DisableHitbox();
    }
    #endregion

    #region Animation Callbacks
    
    /*
    void OnAnimatorMove()
    {
        //Syncs navigation with animation
        //transform.position = navMeshAgent.nextPosition;
        
        Vector3 newRootPosition = new Vector3(anim.rootPosition.x, this.transform.position.y, anim.rootPosition.z);

        transform.position = newRootPosition;
    }
    */
    
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
