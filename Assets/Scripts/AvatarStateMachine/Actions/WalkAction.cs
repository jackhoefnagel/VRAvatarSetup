using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WalkAction : AvatarAction
{
    public Transform destination;

    public override void Act(AvatarStateController avatarStateController)
    {
        StartCoroutine("DoWalk", avatarStateController);
    }

    private IEnumerator DoWalk(AvatarStateController avatarStateController)
    {
        if (Vector3.Distance(avatarStateController.avatarNavMeshAgent.transform.position, destination.position) > 0.4f)
        {
            avatarStateController.avatarNavMeshAgent.destination = destination.position;

            yield return new WaitUntil(() => avatarStateController.avatarNavMeshAgent.pathStatus == NavMeshPathStatus.PathComplete);

            yield return new WaitUntil(() => avatarStateController.avatarNavMeshAgent.remainingDistance < 0.1f);

        }

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

        

        ActionFinished();
    }
}
