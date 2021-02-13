using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{

    public PlayerInputController input;
    public float playerSpeed;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(input.Movement);
        this.transform.position += new Vector3(input.Movement.x, 0, input.Movement.y) * playerSpeed;
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




}
