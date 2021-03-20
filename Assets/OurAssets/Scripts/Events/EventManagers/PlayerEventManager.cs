using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerEventManager : MonoBehaviour
{
    private UnityAction<int> damageEventListener;
    public GameObject player; //replace with player manager later

    void Awake()
    {
        damageEventListener = new UnityAction<int>(damageEventHandler);
    }


    // Use this for initialization
    void Start()
    {



    }


    void OnEnable()
    {
        EventManager.StartListening<DamageEvent, int>(damageEventListener);
    }

    void OnDisable()
    {
        EventManager.StopListening<DamageEvent, int>(damageEventListener);
    }

    void damageEventHandler(int damage)
    {
        player.GetComponent<PlayerStats>().TakeDamage(damage);
    }


}