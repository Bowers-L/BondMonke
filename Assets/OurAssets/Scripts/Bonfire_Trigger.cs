using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire_Trigger : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            BallCollector bc = other.attachedRigidbody.gameObject.GetComponent<BallCollector>();
            if (bc != null)
            {
                //bananaController.SetBool("CloseEnough", true);
            }
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            BallCollector bc = other.attachedRigidbody.gameObject.GetComponent<BallCollector>();
            if (bc != null)
            {
               // bananaController.SetBool("CloseEnough", false);
            }
        }
    }
}
