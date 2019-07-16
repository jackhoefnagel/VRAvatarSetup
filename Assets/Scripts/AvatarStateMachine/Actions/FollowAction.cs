using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class FollowAction : AvatarAction
{

    public override void Act(AvatarStateController avatarStateController)
    {
        avatarStateController.avatarFollowScript.enabled = true;
        ActionFinished();
    }




}
