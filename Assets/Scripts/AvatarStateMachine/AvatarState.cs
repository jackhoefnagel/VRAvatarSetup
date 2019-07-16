using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarState : MonoBehaviour
{
    private AvatarAction[] avatarActions;
    public bool currentlyPerformingState;

    public void Awake()
    {
        avatarActions = transform.GetComponentsInChildren<AvatarAction>();
    }

    public void PerformState(AvatarStateController avatarStateController)
    {
        if(avatarStateController.IsPerformingState() == false) { 
            StartCoroutine("DoPerformState", avatarStateController);
        }
    }

    private IEnumerator DoPerformState(AvatarStateController avatarStateController)
    {
        currentlyPerformingState = true;

        avatarStateController.PerformState(this);

        avatarStateController.avatarFollowScript.enabled = false;

        for (int i = 0; i < avatarActions.Length; i++)
        {
            avatarActions[i].ActionStart();
            avatarActions[i].Act(avatarStateController);
            yield return new WaitUntil(() => avatarActions[i].actionFinished);
        }

        currentlyPerformingState = false;

        avatarStateController.FinishState();
    }
}
