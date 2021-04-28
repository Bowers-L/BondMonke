using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destructible : MonoBehaviour
{
    public GameObject shattered_object;
    public void ShatterCrate()
    {
        Instantiate(shattered_object, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
