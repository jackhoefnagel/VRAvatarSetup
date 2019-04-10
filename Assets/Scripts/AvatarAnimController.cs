using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatarAnimController : MonoBehaviour
{
    public Animator avatarAnimator;
    public NavMeshAgent avatarNavMeshAgent;
    public AvatarHeadLookAt avatarHeadLookAt;
    public Transform avatarHeadLookAtTarget;

    public float walkSpeedSensitivity;
    public float turningSpeedSensitivity;

    private float currentAngularVelocity;
    private Vector3 lastFacing;
    private bool isRotating;

    public void Update()
    {

        Vector3 currentFacing = avatarNavMeshAgent.gameObject.transform.forward;
        currentAngularVelocity = Vector3.SignedAngle(currentFacing, lastFacing, Vector3.up) / Time.deltaTime; //degrees per second
        lastFacing = currentFacing;

        avatarAnimator.SetFloat("avatarSpeed",avatarNavMeshAgent.velocity.magnitude / walkSpeedSensitivity);
        avatarAnimator.SetFloat("avatarTurningSpeed", currentAngularVelocity / turningSpeedSensitivity);
    }

    public void LateUpdate()
    {
        avatarHeadLookAt.LookAt(avatarHeadLookAtTarget);
    }

}
