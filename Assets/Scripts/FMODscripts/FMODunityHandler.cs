using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;
using FMODUnity;
using System;

public class FMODunityHandler : MonoBehaviour
{
    public AvatarAnimationEventsHandler avatarAnimationEventsHandler;

    public FMOD.Studio.EventInstance fmod_footstepEvent;
    public FMODUnity.StudioEventEmitter footstepEventEmitter;
    public FMOD.Studio.EventInstance fmod_clothingrustleEvent;
    public FMODUnity.StudioEventEmitter clothingrustleEventEmitter;
    public FMOD.Studio.EventInstance fmod_sittingEvent;
    public FMODUnity.StudioEventEmitter sittingEventEmitter;
    public FMOD.Studio.EventInstance fmod_standingEvent;
    public FMODUnity.StudioEventEmitter standingEventEmitter;

    [Header("Footstep Variables")]
    public float walkSpeedParamFloat;
    public float turningSpeedFootscuffSensitivityThreshold = 0.5f;
    public enum MovementType { Step, Scuff };
    public MovementType movementType;
    public enum FloorType { Linoleum, Unknown };
    public FloorType floorType;
    public enum ShoeType { Sneakers, Crocs, Slippers, Unknown };
    public ShoeType shoeType;

    [Header("Clothing Variables")]
    public FabricType fabricType;
    public enum FabricType { Cotton, Synthetic, Leather, Denim, Unknown };

    [Header("Sitting Variables")]
    public SittingSurfaceType sittingSurfaceType;
    public enum SittingSurfaceType { Bed, PaddedChair, PlasticChair, WoodenChair };
    public float sittingSoundFadeInTime = 0.5f;
    public float sittingSoundFadeOutTime = 0.5f;

    [Header("Ambience")]
    public List<FMODUnity.StudioEventEmitter> ambienceObjects;
    public WindDirection windDirection;
    public enum WindDirection { North, East, South, West };


    private void OnEnable()
    {


        avatarAnimationEventsHandler.footstep.AddListener(DoFootstep);
        avatarAnimationEventsHandler.footscuff.AddListener(DoFootscuff);
        avatarAnimationEventsHandler.clothingrustleStart.AddListener(DoClothingRustleStart);
        avatarAnimationEventsHandler.clothingrustleStop.AddListener(DoClothingRustleStop);
        avatarAnimationEventsHandler.sitStart.AddListener(DoSitStart);
        avatarAnimationEventsHandler.standStart.AddListener(DoStandStart);

        fmod_footstepEvent = footstepEventEmitter.EventInstance;

        fmod_clothingrustleEvent = clothingrustleEventEmitter.EventInstance;
        fmod_clothingrustleEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

        fmod_sittingEvent = sittingEventEmitter.EventInstance;
        fmod_sittingEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);


        fmod_standingEvent = standingEventEmitter.EventInstance;
        fmod_standingEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);

    }

    private void OnDisable()
    {
        avatarAnimationEventsHandler.footstep.RemoveListener(DoFootstep);
        avatarAnimationEventsHandler.footscuff.RemoveListener(DoFootscuff);
        avatarAnimationEventsHandler.clothingrustleStart.RemoveListener(DoClothingRustleStart);
        avatarAnimationEventsHandler.clothingrustleStop.RemoveListener(DoClothingRustleStop);
    }

    void DoFootstep()
    {
        Set3DAttributes(fmod_footstepEvent, footstepEventEmitter);
        fmod_footstepEvent.setParameterByName("WalkingSpeed", avatarAnimationEventsHandler.currentWalkingSpeed);
        movementType = MovementType.Step;
        fmod_footstepEvent.setParameterByName("MovementType", (int)movementType);
        fmod_footstepEvent.setParameterByName("FloorType", (int)floorType);
        fmod_footstepEvent.setParameterByName("ShoeType", (int)shoeType);

        RESULT result = fmod_footstepEvent.start();
    }

    void DoFootscuff()
    {
        Set3DAttributes(fmod_footstepEvent, footstepEventEmitter);
        fmod_footstepEvent.setParameterByName("WalkingSpeed", avatarAnimationEventsHandler.currentWalkingSpeed);
        movementType = MovementType.Scuff;
        fmod_footstepEvent.setParameterByName("MovementType", (int)movementType);
        fmod_footstepEvent.setParameterByName("FloorType", (int)floorType);
        fmod_footstepEvent.setParameterByName("ShoeType", (int)shoeType);

        if (Mathf.Abs(avatarAnimationEventsHandler.currentTurningSpeed) > turningSpeedFootscuffSensitivityThreshold)
        {
            fmod_footstepEvent.start();
        }
              
    }

    void DoClothingRustleStart()
    {
        Set3DAttributes(fmod_clothingrustleEvent, clothingrustleEventEmitter);
        fmod_clothingrustleEvent.setParameterByName("ClothingSpeed", UnityEngine.Random.Range(0.1f, 0.9f));
        fmod_clothingrustleEvent.start();
    }


    void DoClothingRustleStop()
    {
        fmod_clothingrustleEvent.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
    }

    void DoSitStart()
    {
        Set3DAttributes(fmod_sittingEvent, sittingEventEmitter);
        fmod_sittingEvent.start();
    }

    void DoStandStart()
    {
        Set3DAttributes(fmod_standingEvent, standingEventEmitter);
        fmod_standingEvent.start();
    }


    private void Set3DAttributes(FMOD.Studio.EventInstance instanceToSet, FMODUnity.StudioEventEmitter emitterToSet)
    {
        ATTRIBUTES_3D attr_3D = FMODUnity.RuntimeUtils.To3DAttributes(emitterToSet.gameObject);
        instanceToSet.set3DAttributes(attr_3D);
    }
}
