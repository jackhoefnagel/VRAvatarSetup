using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class StandAction : AvatarAction
{
    public override void Act(AvatarStateController avatarStateController)
    {
        if (CheckIfSitting(avatarStateController))
        {
            StartCoroutine("DoStandAtPosition", avatarStateController);
        }
        else
        {
            ActionFinished();
        }
    }

    private bool CheckIfSitting(AvatarStateController avatarStateController)
    {
        return avatarStateController.avatarAnimator.GetBool("sit");
    }

    private IEnumerator DoStandAtPosition(AvatarStateController avatarStateController)
    {
        avatarStateController.avatarAnimator.SetBool("sit",false);

        yield return new WaitForSeconds(1.5f);

        ActionFinished();
    }


}
