﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AvatarAnimationEventsHandler : MonoBehaviour
{
    [HideInInspector]
    public UnityEvent footstep, footscuff;
    [HideInInspector]
    public UnityEvent clothingrustleStart, clothingrustleStop, sitStart, standStart;

    public float blinkIntervalSeconds = 3f;
    private float blinkTimer = 0f;

    public Animator animator;
    public float currentWalkingSpeed;
    public float currentTurningSpeed;

    public void Update()
    {
        blinkTimer += Time.deltaTime;
        if(blinkTimer > blinkIntervalSeconds)
        {
            animator.SetTrigger("shouldBlink");
            blinkTimer = 0f;
        }
    }

    public void DoFootstep() {
        GetFootstepParams();
        footstep.Invoke();
    }

    public void DoFootscuff()
    {
        GetFootstepParams();
        footscuff.Invoke();
    }

    public void DoClothingRustle(float clothingRustleSpeed)
    {
        clothingrustleStart.Invoke();
    }

    public void StopClothingRustle()
    {
        clothingrustleStop.Invoke();
    }

    private void GetFootstepParams()
    {
        currentWalkingSpeed = animator.GetFloat("avatarSpeed");
        currentTurningSpeed = animator.GetFloat("avatarTurningSpeed");
    }

    public void DoSit()
    {
        sitStart.Invoke();
    }

    public void DoStand()
    {
        standStart.Invoke();
    }

}
