using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AvatarAction : MonoBehaviour
{
    [HideInInspector]
    public bool actionFinished;

    public abstract void Act(AvatarStateController avatarStateController);

    public void ActionStart()
    {
        actionFinished = false;
    }

    public void ActionFinished()
    {
        actionFinished = true;
    }
}
