using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCollector : MonoBehaviour
{
    public int hasSkillPoint = 0;
    public int healthIncrease;
    public bool final = false;
    public GameObject canvas;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ReceiveBall()
    {
        hasSkillPoint++;

        //Increase player health?
        PlayerStats stats = GetComponent<PlayerStats>();
        if (stats)
        {
            stats.TakeDamage(-healthIncrease);
        } else
        {
            Debug.LogWarning("No Stats Component on BallCollector");
        }
        
    }
    public void ReceiveFinal()
    {
        final = true;
    }
}
