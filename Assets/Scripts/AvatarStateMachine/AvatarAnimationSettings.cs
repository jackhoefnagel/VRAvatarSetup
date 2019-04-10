using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AvatarAnimInt
{
    public string animIntName;
    public int animIntToSet;
}

[System.Serializable]
public class AvatarAnimFloat
{
    public string animFloatName;
    public float animFloatToSet;
}

[CreateAssetMenu(menuName = "AvatarAnimationSettings")]
public class AvatarAnimationSettings : ScriptableObject
{
    public string animBoolTrue;
    public string animBoolFalse;
    public AvatarAnimInt animInt;
    public AvatarAnimFloat animFloat;
    public string animTrigger;

    public void SetAnimationSettings(AvatarStateController avatarStateController)
    {
        if (animBoolTrue != "")
            avatarStateController.avatarAnimator.SetBool(animBoolTrue, true);

        if (animBoolFalse != "")
            avatarStateController.avatarAnimator.SetBool(animBoolFalse, false);

        if (animInt.animIntName != "")
            avatarStateController.avatarAnimator.SetInteger(animInt.animIntName, animInt.animIntToSet);

        if (animFloat.animFloatName != "")
            avatarStateController.avatarAnimator.SetFloat(animFloat.animFloatName, animFloat.animFloatToSet);

        if (animTrigger != "")
            avatarStateController.avatarAnimator.SetTrigger(animTrigger);
    }
}
