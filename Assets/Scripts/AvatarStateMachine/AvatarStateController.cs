using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatarStateController : MonoBehaviour
{
    public AvatarState currentAvatarState;
    public Animator avatarAnimator;
    public AvatarAnimController avatarAnimatorController;
    public NavMeshAgent avatarNavMeshAgent;
    public AvatarFollowScript avatarFollowScript;

    public Transform nextDestination;

    public void PerformState(AvatarState avatarState)
    {
        if(currentAvatarState == null)
        {
            currentAvatarState = avatarState;
            currentAvatarState.PerformState(this);
        }
    }

    public bool IsPerformingState()
    {
        return currentAvatarState != null;
    }

    public void FinishState()
    {
        currentAvatarState = null;
    }
}
