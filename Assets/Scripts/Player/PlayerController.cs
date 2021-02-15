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

    private static int blockTimes = 0;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();

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
            Debug.Log("Player is blocking: " + blockTimes);
            blockTimes++;
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
    }

    public void OnHeavyAttack()
    {
        Debug.Log("Player uppercut");
    }

    void OnAnimatorMove()
    {

        Vector3 newRootPosition = new Vector3(animator.rootPosition.x, this.transform.position.y, animator.rootPosition.z);
        Quaternion newRootRotation = animator.rootRotation;


        newRootPosition = Vector3.LerpUnclamped(this.transform.position, newRootPosition, rootMotionMovementSpeed);

        this.transform.position = newRootPosition;
        this.transform.rotation = newRootRotation;
    }


}
