using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandIK : MonoBehaviour
{
    public enum HandTarget { Idle, Button, FoldingArms };
    private HandTarget handTarget = HandTarget.Idle;

    public Transform handIKTarget;

    public Transform idleHandIKPosition;
    public Transform foldingArmsIKPosition;
    public Transform buttonIKPosition;

    private Transform transformToAnimateTo;


    public void MoveHandToTarget(float timeToTarget, HandTarget newHandTarget)
    {
        handTarget = newHandTarget;
        StartCoroutine("DoMoveHandToTarget", timeToTarget);
    }

    private IEnumerator DoMoveHandToTarget(float moveTime)
    {
        switch (handTarget)
        {
            case HandTarget.Idle:
                transformToAnimateTo = idleHandIKPosition;
                break;
            case HandTarget.FoldingArms:
                transformToAnimateTo = foldingArmsIKPosition;
                break;
            case HandTarget.Button:
                transformToAnimateTo = buttonIKPosition;
                break;
            default:
                transformToAnimateTo = idleHandIKPosition;
                break;
        }

        float animTime = moveTime;
        float animTimer = 0.0f;

        float percentage;

        while(animTimer < animTime)
        {
            percentage = animTimer / animTime;
            handIKTarget.position = Vector3.Lerp(handIKTarget.position, transformToAnimateTo.position, percentage);
            handIKTarget.rotation = Quaternion.Lerp(handIKTarget.rotation, transformToAnimateTo.rotation, percentage);
            animTimer += Time.deltaTime;
            yield return null;
        }
    }
}
