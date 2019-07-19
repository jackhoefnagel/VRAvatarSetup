using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AvatarAnimationEventsHandler : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent footstep, footscuff;

    public Animator animator;
    public float currentWalkingSpeed;
    public float currentTurningSpeed;

    public void DoFootstep() {
        GetFootstepParams();
        footstep.Invoke();
    }

    public void DoFootscuff()
    {
        GetFootstepParams();
        footscuff.Invoke();
    }

    private void GetFootstepParams()
    {
        currentWalkingSpeed = animator.GetFloat("avatarSpeed");
        currentTurningSpeed = animator.GetFloat("avatarTurningSpeed");

    }

}
