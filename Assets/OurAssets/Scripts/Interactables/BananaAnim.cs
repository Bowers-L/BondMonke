using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaAnim : MonoBehaviour
{
    // Start is called before the first frame update
        [SerializeField] private Animator bananaController;
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
        {
            BallCollector bc = other.attachedRigidbody.gameObject.GetComponent<BallCollector>();
            if (bc != null)
            {
                bananaController.SetBool("CloseEnough", true);
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
                bananaController.SetBool("CloseEnough", false);
            }
        }
    }
}
