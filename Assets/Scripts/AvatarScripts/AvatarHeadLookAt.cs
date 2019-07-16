using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarHeadLookAt : MonoBehaviour
{
    public enum HeadLookatTargetType { EyeToEye, LookAway, LookAwayGround, LookForward };
    public HeadLookatTargetType currentHeadLookatTargetType;

    public AvatarAnimController avatarAnimController;

    public Transform headLookatTarget;
    public Transform eyeToEyeTransform;
    public Transform lookAwayTransform;
    public Transform lookAwayGroundTransform;
    public Transform lookForwardTransform;

    public Transform newTarget;
    private Vector3 previousLookatPosition;

    public void LookAt(HeadLookatTargetType newHeadLookatTargetType)
    {
        if (newHeadLookatTargetType != currentHeadLookatTargetType) {

            switch (newHeadLookatTargetType)
            {
                case HeadLookatTargetType.EyeToEye:
                    newTarget = eyeToEyeTransform;
                    break;
                case HeadLookatTargetType.LookAway:
                    newTarget = lookAwayTransform;
                    break;
                case HeadLookatTargetType.LookAwayGround:
                    newTarget = lookAwayGroundTransform;
                    break;
                case HeadLookatTargetType.LookForward:
                    newTarget = lookForwardTransform;
                    break;
            }

            StartCoroutine("DoLook");

            currentHeadLookatTargetType = newHeadLookatTargetType;
        }
    }

    private IEnumerator DoLook()
    {
        float animTime = 1f;
        float animTimer = 0.0f;

        float percentage;

        previousLookatPosition = headLookatTarget.position;

        while (animTimer < animTime)
        {
            percentage = animTimer / animTime;
            headLookatTarget.position = Vector3.Lerp(previousLookatPosition, newTarget.position, percentage);
            animTimer += Time.deltaTime;
            yield return null;
        }

        //headLookatTarget = newTarget;
        avatarAnimController.avatarHeadLookAtTarget = newTarget;
    }
}
