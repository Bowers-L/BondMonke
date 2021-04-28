using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableDropper : MonoBehaviour
{
    public GameObject dropped;
    public Transform dropLocation;

    public void DropCollectable()
    {
        GameObject.Instantiate(dropped, dropLocation.position, dropLocation.rotation);
    }
}
