using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField] private Animator elevatorController;

    private void Awake()
    {
        if (elevatorController == null)
        {
            elevatorController = GetComponentInParent<Animator>();
            if (elevatorController == null)
            {
                Debug.LogError("Elevator does not have animator");
            }
        }
        elevatorController.SetBool("TutEnemyDefeated", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.position.y == -1.75)
        {
            elevatorController.SetBool("AtGround", true);
        } else
        {
            elevatorController.SetBool("AtGround", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elevatorController.SetBool("PlayerOn", true);
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            elevatorController.SetBool("PlayerOn", false);
            other.transform.parent = null;
        }
    }
}
