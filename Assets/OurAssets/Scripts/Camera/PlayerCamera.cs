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
    public float lookSpd = 0.03f;
    public float followSpd = 0.1f;
    public float pivotSpd = 0.03f;

    private float targetPos;
    private float defaultPos;
    private float lookAngle;
    private float pivotAngle;
    private float minPivot = -35;
    public float maxPivot = 35;

    public float camera_radius = 0.2f;
    public float camera_offset = 0.2f;
    public float min_camera_offset = 0.2f;
    public float smoothingPosSpeed = 1.0f;
    public float smoothingPosMaxSpeed = 100.0f;
    public float smoothingRotSpeed = 1.0f;
    public float smoothingRotMaxSpeed = 100.0f;


    private Vector3 currVelForSmoothing;
    private Quaternion currRotForSmoothing;

    private void Awake()
    {
        ignorelayers = 1 << 10;
        
        singleton = this;
        defaultPos = cameraTransform.localPosition.z;
        // ignore layers specificity

        if (playerTransform == null)
        {
            GameObject player = GameObject.Find("Player");
            if (player == null)
            {
                Debug.LogError("Can't find the player");
            } else
            {
                playerTransform = player.transform;
            }
        }
        Debug.Log("Player camera awoke");
    }

    public void FollowTarget(float delta)
    {
        //Debug.Log("stuff1");
        Vector3 targetPos = Vector3.SmoothDamp(this.transform.position, playerTransform.position, ref camera_velocity, delta / followSpd);
        this.transform.position = targetPos;

        CameraCollision(delta);
    }

    public void CameraRotation(float delta, float mouseX, float mouseY)
    {
        //Change the look angle to lock on to the enemy if there is one.
        CombatAgent playerLockOn = playerTransform.GetComponent<PlayerController>().lockOn;
        if (playerLockOn == null)
        {
            //free rotation
            // Debug.Log("udbfnuioerbgnfuisbngurieebns");
            // take in free mouse input and change camera orientation based on inputs
            lookAngle += (mouseX * lookSpd) / delta;

        } else
        {
            Quaternion lookAtEnemy = Quaternion.LookRotation(playerLockOn.transform.position - playerTransform.position, Vector3.up);
            lookAngle = lookAtEnemy.eulerAngles.y;
        }

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

    private void CameraCollision(float delta)
    {
        targetPos = defaultPos;
        RaycastHit hit;
        Vector3 cameradir = cameraTransform.position - cameraPivotTransform.position;
        cameradir.Normalize();

        if (Physics.SphereCast(cameraPivotTransform.position, camera_radius, cameradir, out hit, Mathf.Abs(targetPos), ignorelayers))
        {
            float dist = Vector3.Distance(cameraPivotTransform.position, hit.point);
            targetPos = -(dist - camera_offset);

        }

        if (Mathf.Abs(targetPos) < min_camera_offset)
        {
            targetPos = -min_camera_offset;
        }

        cameraTransformPos.z = Mathf.Lerp(cameraTransform.localPosition.z, targetPos, delta / 0.2f);
        cameraTransform.localPosition = cameraTransformPos;
    }
}
