using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    public Transform playerTransform;
    public Transform followTrans;

    public float smoothingPosSpeed = 1.0f;
    public float smoothingPosMaxSpeed = 100.0f;
    public float smoothingRotSpeed = 1.0f;
    public float smoothingRotMaxSpeed = 100.0f;


    private Vector3 currVelForSmoothing;
    private Quaternion currRotForSmoothing;

    private void LateUpdate()
    {
        //smoothly transition to follow the player
        this.transform.position = Vector3.SmoothDamp(this.transform.position, followTrans.position, ref currVelForSmoothing, smoothingPosSpeed, smoothingPosMaxSpeed);
        this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.LookRotation(playerTransform.position - followTrans.position + Vector3.up*1, Vector3.up), smoothingRotSpeed*Time.deltaTime);
    }
}
