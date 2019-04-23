using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAction : AvatarAction
{
    public AvatarHeadLookAt.HeadLookatTargetType lookatTargetType;

    public override void Act(AvatarStateController avatarStateController)
    {
        avatarStateController.avatarAnimatorController.avatarHeadLookAt.LookAt(lookatTargetType);
        ActionFinished();
    }
}
