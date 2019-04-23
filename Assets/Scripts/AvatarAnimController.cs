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

    public HandIK leftHandIK;
    public HandIK rightHandIK;

    [Range(0, 1)]
    public float lookWeight;
    [Range(0, 1)]
    public float headLookWeight;
    [Range(0, 1)]
    public float eyesLookWeight;
    [Range(0, 1)]
    public float bodyLookWeight;
    public float clampWeight;

    public bool doHandsIK;
    [Range(0, 1)]
    public float handsIKweight;
    private float targetHandsIKweight;

    private void Start()
    {

    }

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
        //avatarHeadLookAt.LookAt(avatarHeadLookAtTarget);
    }



    private void OnAnimatorIK()
    {
        avatarAnimator.SetLookAtWeight(lookWeight, bodyLookWeight, headLookWeight, eyesLookWeight, clampWeight);
        avatarAnimator.SetLookAtPosition(avatarHeadLookAtTarget.position);

        // Hands IK positioning
        if (doHandsIK)
        {
            avatarAnimator.SetIKPositionWeight(AvatarIKGoal.LeftHand, handsIKweight);
            avatarAnimator.SetIKPositionWeight(AvatarIKGoal.RightHand, handsIKweight);
            avatarAnimator.SetIKRotationWeight(AvatarIKGoal.LeftHand, handsIKweight);
            avatarAnimator.SetIKRotationWeight(AvatarIKGoal.RightHand, handsIKweight);
            avatarAnimator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandIK.handIKTarget.position);
            avatarAnimator.SetIKPosition(AvatarIKGoal.RightHand, rightHandIK.handIKTarget.position);
            avatarAnimator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandIK.handIKTarget.rotation);
            avatarAnimator.SetIKRotation(AvatarIKGoal.RightHand, rightHandIK.handIKTarget.rotation);
        }       
    }

    public void MoveHands(float handsMoveTime, HandIK.HandTarget handTargetType)
    {
        handsIKweight = 0f;
        doHandsIK = true;
        targetHandsIKweight = 1f;
        StartCoroutine("ChangeIKWeight", handsMoveTime);
        leftHandIK.MoveHandToTarget(1f, handTargetType);
        rightHandIK.MoveHandToTarget(1f, handTargetType);
    }

    public void StopMovingHands(float handsMoveTime)
    {
        targetHandsIKweight = 0f;
        StartCoroutine("ChangeIKWeight", handsMoveTime);
        leftHandIK.MoveHandToTarget(1f, HandIK.HandTarget.Idle);
        rightHandIK.MoveHandToTarget(1f, HandIK.HandTarget.Idle);
    }

    private IEnumerator ChangeIKWeight(float timeToTarget)
    {
        float animTime = timeToTarget;
        float animTimer = 0.0f;

        float percentage;
        float previousIKweight = handsIKweight;

        while (animTimer < animTime)
        {
            percentage = animTimer / animTime;
            handsIKweight = Mathf.Lerp(previousIKweight, targetHandsIKweight, percentage);
            animTimer += Time.deltaTime;
            yield return null;
        }
    }

}
