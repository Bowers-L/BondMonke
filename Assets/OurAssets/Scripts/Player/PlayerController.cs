using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public bool enteredBonfire;
    public float animationSpeed = 1.0f;
    public float rootMotionMovementSpeed = 1.0f;
    public float turnSpeed = 1.0f;
    private bool isGrounded;

    //Attacks
    public int lightAttackDamage;
    public int lightAttackCost; // for Stamina

    public int heavyAttackDamage;
    public int heavyAttackCost; // for Stamina
    
    private PlayerInputController input;
    private CombatAgent combat;
    public PlayerCamera player_camera;
    public DamageCollider fist;
    private Animator animator;
    private Rigidbody rb;
    private PlayerStats stats;
    public Vector3 respawnPoint;

    [SerializeField]
    private HurtBoxMarker hurtBox;
    [SerializeField]
    private CapsuleCollider capsule;

    public float killPlaneY = -10.0f;

    private void Awake()
    {
        
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();

            if (animator == null)
            {
                Debug.LogError("Player is missing animator component.");
            }
        }

        combat = GetComponent<CombatAgent>();
        if (combat == null)
        {
            Debug.LogError("Player is missing CombatAgent component");
        }

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Player is missing rigidbody component.");
        }

        capsule = GetComponent<CapsuleCollider>();
        if (capsule == null)
        {
            Debug.LogError("Player is missing rigidbody component.");
        }

        if (!hurtBox)
        {
            hurtBox = GetComponentInChildren<HurtBoxMarker>();
        }

        input = GetComponent<PlayerInputController>();
        if (input == null)
        {
            Debug.LogError("Player is missing PlayerInputController component.");
        }

        stats = GetComponent<PlayerStats>();
        if (stats == null)
        {
            Debug.LogError("Player is missing PlayerStats component.");
        }

        //disable the death fader to start with
        DeathFader fader = GetComponentInChildren<DeathFader>();
        fader.enabled = false;  //start with the enemy

        animator.applyRootMotion = true;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.speed = animationSpeed;
        respawnPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //ground check
        float distToGround = this.GetComponent<Collider>().bounds.extents.y;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, distToGround + .1f);
        input.TickInput();

        //fall check
        if (transform.position.y <= killPlaneY)
        {
            stats.TakeDamage(stats.current_health);
            Die();
        }

        //Set animation parameters
        animator.SetFloat("MovementX", input.Movement.x);
        animator.SetFloat("MovementY", input.Movement.y);
        animator.SetBool("Sprint", input.Sprint);
        animator.SetBool("Block", input.Block);

        //Disable the hurtbox if the player is blocking
        hurtBox.GetComponent<CapsuleCollider>().enabled = !input.Block;

        //Render the visible hurtbox for debug purposes.
        fist.GetComponent<MeshRenderer>().enabled = GameManager.Instance.debugMode;
        hurtBox.GetComponent<MeshRenderer>().enabled = GameManager.Instance.debugMode;
        if (stats.current_health <= 0)
        {
            Die();
        }
    }

    //Disable Player's Input map
    public void DisableInput()
    {
        input.enabled = false;
    }

    /*
     * Events triggered on input
     */
    #region Player Move Events
    public void OnDodge()
    {
        if (isGrounded)
        {
            Debug.Log("Player dodged");
            animator.SetTrigger("Roll");
        }
    }

    public void OnLightAttack()
    {
        if (stats.current_stamina > 0)
        {
            Debug.Log("Player punched");
            animator.SetTrigger("LightAttack");
            combat.SetDamage(fist, lightAttackDamage); //call this as animation event
            stats.StaminaCost(lightAttackCost);
        }
    }

    public void OnHeavyAttack()
    {
        if (stats.current_stamina > 0)
        {
            Debug.Log("Player uppercut");
            animator.SetTrigger("HeavyAttack");
            combat.SetDamage(fist, heavyAttackDamage);
            stats.StaminaCost(heavyAttackCost);
        }
    }

    public void OnInteract()
    {
        Debug.Log("Player interacted");
    }
    #endregion

    /*
     * Events triggered by a player animation
     */
    #region Player Move Animation Events

    public void OnRollEnter()
    {
        //rolling makes the player collider smaller so
        //the player can move under obstacles and avoid enemies more easily.
        capsule.height /= 2.0f;
        //capsule.center = new Vector3(capsule.center.x, capsule.center.y * 0.9f, capsule.center.z);
        hurtBox.transform.localScale = new Vector3(hurtBox.transform.localScale.x, hurtBox.transform.localScale.y / 2, hurtBox.transform.localScale.z);
    }

    public void OnRollExit()
    {
        capsule.height *= 2.0f;
        //capsule.center = new Vector3(capsule.center.x, capsule.center.y / 0.9f, capsule.center.z);
        hurtBox.transform.localScale = new Vector3(hurtBox.transform.localScale.x, hurtBox.transform.localScale.y * 2, hurtBox.transform.localScale.z);
    }

    #endregion

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bonfire"))
            enteredBonfire = true;

        if (other.CompareTag("PromptTrigger"))
        {
            PromptTrigger pt = other.GetComponent<PromptTrigger>();
            pt.enableText();
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bonfire"))
            enteredBonfire = false;

        if (other.CompareTag("PromptTrigger"))
        {
            PromptTrigger pt = other.GetComponent<PromptTrigger>();
            pt.disableText();
        }
    }
    /*
     * Animator callback
     */
    void OnAnimatorMove()
    {
        //Change root motion position based on parameters
        Vector3 newRootPosition = new Vector3(animator.rootPosition.x, this.transform.position.y, animator.rootPosition.z);
        


        newRootPosition = Vector3.LerpUnclamped(this.transform.position, newRootPosition, rootMotionMovementSpeed);

        this.transform.position = newRootPosition;
        //this.transform.rotation = newRootRotation;

        transform.RotateAround(transform.position, Vector3.up, turnSpeed * animator.GetFloat("MovementX"));  //doing rotation programmatically

        //Change the transitions
    }


    public void EnableFistCollider()
    {
        combat.StartAttack(fist);
    }

    public void DisableFistCollider()
    {
        combat.FinishAttack();
    }

    public void Die()
    {
        enabled = false;
        if (GetComponentInChildren<DeathFader>() == null)
        {
            Debug.Log("DeathFader not added to enemy mesh");
        }
        else
        {
            GetComponentInChildren<DeathFader>().enabled = true;
        }

        //Death Animation
        animator.SetTrigger("Death");
    }
}
