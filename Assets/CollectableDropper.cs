using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableDropper : MonoBehaviour
{
    public GameObject dropped;

    public void DropCollectable()
    {
        GameObject.Instantiate(dropped, new Vector3(transform.position.x, transform.position.y + 1.0f, transform.position.z), Quaternion.identity);
    }
}
