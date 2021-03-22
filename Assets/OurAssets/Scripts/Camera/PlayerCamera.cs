using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public Transform playerTransform;
    public Transform cameraTransform;
    public Transform cameraPivotTransform;
    private Vector3 cameraTransformPos;
    private LayerMask ignorelayers;
    public Vector3 camera_velocity = Vector3.zero;

    public static PlayerCamera singleton;
    public float lookSpd = 0.1f;
    public float followSpd = 0.1f;
    public float pivotSpd = 0.03f;

    private float defaultPos;
    private float lookAngle;
    private float pivotAngle;
    private float minPivot = -35;
    public float maxPivot = 35;

    public float smoothingPosSpeed = 1.0f;
    public float smoothingPosMaxSpeed = 100.0f;
    public float smoothingRotSpeed = 1.0f;
    public float smoothingRotMaxSpeed = 100.0f;


    private Vector3 currVelForSmoothing;
    private Quaternion currRotForSmoothing;

    private void Awake()
    {
        singleton = this;
        defaultPos = cameraTransform.localPosition.z;
        // ignore layers specificity

        
    }

    public void FollowTarget(float delta)
    {
        Debug.Log("stuff1");
        Vector3 targetPos = Vector3.SmoothDamp(this.transform.position, playerTransform.position, ref camera_velocity, delta / followSpd);
        this.transform.position = targetPos;
    }

    public void CameraRotation(float delta, float mouseX, float mouseY)
    {
        Debug.Log("udbfnuioerbgnfuisbngurieebns");
        // take in free mouse input and change camera orientation based on inputs
        lookAngle += (mouseX * lookSpd) / delta;
        pivotAngle -= (mouseY * pivotSpd) / delta;
        pivotAngle = Mathf.Clamp(pivotAngle, minPivot, maxPivot);

        Vector3 rotation = Vector3.zero;
        rotation.y = lookAngle;
        Quaternion targetRot = Quaternion.Euler(rotation);
        this.transform.rotation = targetRot;

        rotation = Vector3.zero;
        rotation.x = pivotAngle;

        targetRot = Quaternion.Euler(rotation);
        cameraPivotTransform.localRotation = targetRot;
    }
}
