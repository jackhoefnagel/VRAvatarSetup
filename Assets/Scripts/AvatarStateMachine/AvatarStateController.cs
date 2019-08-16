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

    public StatesUIPanel statesUIpanel;

    public Transform nextDestination;


    [Header("Time until state should finish automatically")]
    public float stateAutoFinishTime = 7.0f;


    public void PerformState(AvatarState avatarState)
    {
        if(currentAvatarState == null)
        {
            currentAvatarState = avatarState;
            currentAvatarState.PerformState(this);
            statesUIpanel.SwitchPanelButtonActivation(false);
            StartCoroutine("DoAutoActivationTimer");
        }
    }

    private IEnumerator DoAutoActivationTimer()
    {
        yield return new WaitForSeconds(stateAutoFinishTime);
        FinishState();
    }

    public bool IsPerformingState()
    {
        //TODO: state logging clearer
        bool hasCurrentAvatarState = false;
        if(currentAvatarState == null)
        {

        }
        return currentAvatarState != null;
    }

    public void FinishState()
    {
        StopCoroutine("DoAutoActivationTimer");
        statesUIpanel.SwitchPanelButtonActivation(true);
        currentAvatarState = null;
    }
}
