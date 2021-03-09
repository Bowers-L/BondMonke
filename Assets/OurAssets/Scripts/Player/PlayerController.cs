using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public PlayerInputController input;
    public Animator animator;
    public Rigidbody rb;

    public float animationSpeed;
    public float rootMotionMovementSpeed;
    public float turnSpeed;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            animator = GetComponentInChildren<Animator>();
        }
        if (animator == null)
        {
            Debug.LogError("Player is missing animator component.");
        }

        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Player is missing rigidbody component.");
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
        animator.SetFloat("MovementX", input.Movement.x);
        animator.SetFloat("MovementY", input.Movement.y);
        animator.SetBool("Sprint", input.Sprint);

        if (input.Block)
        {
            Debug.Log("Player is blocking.");
        }
    }

    /*
     * Input Callback Functions
     */

    public void OnDodge()
    {
        Debug.Log("Player dodged");
    }

    public void OnLightAttack()
    {
        Debug.Log("Player punched");
        animator.SetTrigger("Attack");
    }

    public void OnHeavyAttack()
    {
        Debug.Log("Player uppercut");
    }

    public void OnInteract()
    {
        Debug.Log("Player interacted");
    }

    void OnAnimatorMove()
    {
        Vector3 newRootPosition = new Vector3(animator.rootPosition.x, this.transform.position.y, animator.rootPosition.z);
        


        newRootPosition = Vector3.LerpUnclamped(this.transform.position, newRootPosition, rootMotionMovementSpeed);

        this.transform.position = newRootPosition;
        //this.transform.rotation = newRootRotation;

        transform.RotateAround(transform.position, Vector3.up, turnSpeed * animator.GetFloat("MovementX"));  //doing rotation programmatically
    }


}
