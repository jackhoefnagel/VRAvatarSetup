using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmMoveAction : AvatarAction
{
    public HandIK.HandTarget handTarget;
    public bool shouldStopMovingArms;
    public float transitionTime = 0.75f;

    public override void Act(AvatarStateController avatarStateController)
    {
        if (shouldStopMovingArms)
        {
            avatarStateController.avatarAnimatorController.StopMovingHands(transitionTime);
        }
        else
        {
            avatarStateController.avatarAnimatorController.MoveHands(transitionTime, handTarget);
        }


        ActionFinished();
    }
}
