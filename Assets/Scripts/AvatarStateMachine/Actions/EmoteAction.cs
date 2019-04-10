using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteAction : AvatarAction
{
    public AvatarAnimationSettings animationSettings;

    public override void Act(AvatarStateController avatarStateController)
    {
        animationSettings.SetAnimationSettings(avatarStateController);

        ActionFinished();
    }
}
