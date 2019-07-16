using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectScriptAction : AvatarAction
{
    public UnityEvent objectScriptEvent;

    public float delayUntilAction = 0.0f;

    public override void Act(AvatarStateController avatarStateController)
    {
        StartCoroutine(DoAct(avatarStateController));
    }

    private IEnumerator DoAct(AvatarStateController avatarStateController)
    {
        yield return new WaitForSeconds(delayUntilAction);
        objectScriptEvent.Invoke();
        ActionFinished();
    }
}
