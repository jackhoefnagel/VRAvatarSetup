using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;

public class SitAction : AvatarAction
{
    public Transform destination;

    public override void Act(AvatarStateController avatarStateController)
    {
        if (!CheckIfSitting(avatarStateController)){
            StartCoroutine("DoSitAtPosition", avatarStateController);
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

    private IEnumerator DoSitAtPosition(AvatarStateController avatarStateController)
    {

        avatarStateController.avatarNavMeshAgent.destination = destination.position;

        yield return new WaitUntil(() => avatarStateController.avatarNavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete);

        yield return new WaitUntil(() => avatarStateController.avatarNavMeshAgent.remainingDistance < 0.1f);

        float timer = 1.0f;
        float time = 0.0f;
        float percentage = 0.0f;

        Quaternion oldRot = avatarStateController.avatarNavMeshAgent.transform.rotation;

        while (time < timer)
        {
            percentage = time / timer;

            avatarStateController.avatarNavMeshAgent.transform.rotation = Quaternion.Slerp(oldRot, destination.rotation, percentage);

            time += Time.deltaTime;
            yield return 0;
        }

        avatarStateController.avatarNavMeshAgent.transform.rotation = destination.rotation;

        avatarStateController.avatarAnimator.SetBool("sit", true);

        ActionFinished();
    }


}
