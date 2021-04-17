using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalCollectable : MonoBehaviour
{
    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            BallCollector bc = c.attachedRigidbody.gameObject.GetComponent<BallCollector>();
            if (bc != null)
            {
                bc.ReceiveFinal();
                Destroy(this.gameObject);
            }
        }
    }
}
