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

    //Attacks
    public int lightAttackDamage;
    public int heavyAttackDamage;
    
    private PlayerInputController input;
    private CombatAgent combat;
    public PlayerCamera player_camera;
    public DamageCollider fist;
    private Animator animator;
    private Rigidbody rb;
    private PlayerStats stats;

    [SerializeField]
    private HurtBoxMarker hurtBox;
    [SerializeField]
    private CapsuleCollider capsule;

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

        animator.applyRootMotion = true;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.speed = animationSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        input.TickInput();
        // Debug.Log("mouse " + input.CameraInput);
        animator.SetFloat("MovementX", input.Movement.x);
        animator.SetFloat("MovementY", input.Movement.y);
        animator.SetBool("Sprint", input.Sprint);
        animator.SetBool("Block", input.Block);

        if (input.Block)
        {
            //Debug.Log("Player is blocking.");
        }

        //Render the visible hurtbox for debug purposes.
        fist.GetComponent<MeshRenderer>().enabled = GameManager.Instance.debugMode;
        hurtBox.GetComponent<MeshRenderer>().enabled = GameManager.Instance.debugMode;

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
        Debug.Log("Player dodged");
        animator.SetTrigger("Roll");
    }

    public void OnLightAttack()
    {
        Debug.Log("Player punched");
        animator.SetTrigger("LightAttack");
        combat.SetDamage(fist, lightAttackDamage); //call this as animation event
    }

    public void OnHeavyAttack()
    {
        Debug.Log("Player uppercut");
        animator.SetTrigger("HeavyAttack");
        combat.SetDamage(fist, heavyAttackDamage);  
        //EventManager.TriggerEvent<DamageEvent, int>(-1); //Only for testing purposes
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
        if (other.tag == "Bonfire")
            enteredBonfire = true;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Bonfire")
            enteredBonfire = false;
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

}
