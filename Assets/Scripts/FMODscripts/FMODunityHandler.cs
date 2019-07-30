using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class FMODunityHandler : MonoBehaviour
{
    public AvatarAnimationEventsHandler avatarAnimationEventsHandler;

    public FMOD.Studio.EventInstance fmod_footstepEvent;
    public FMODUnity.StudioEventEmitter footstepEventEmitter;
    public FMOD.Studio.EventInstance fmod_clothingrustleEvent;
    public FMODUnity.StudioEventEmitter clothingrustleEventEmitter;

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

    [Header("Ambience")]
    public List<FMODUnity.StudioEventEmitter> ambienceObjects;
    public WindDirection windDirection;
    public enum WindDirection { North, East, South, West };

    private void Update()
    {

    }

    private void OnEnable()
    {
        avatarAnimationEventsHandler.footstep.AddListener(DoFootstep);
        avatarAnimationEventsHandler.footscuff.AddListener(DoFootscuff);
        avatarAnimationEventsHandler.clothingrustleStart.AddListener(DoClothingRustleStart);
        avatarAnimationEventsHandler.clothingrustleStop.AddListener(DoClothingRustleStop);

        fmod_footstepEvent = footstepEventEmitter.EventInstance;
        fmod_clothingrustleEvent = clothingrustleEventEmitter.EventInstance;

        fmod_footstepEvent.triggerCue();
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
        fmod_footstepEvent.setParameterByName("WalkingSpeed", avatarAnimationEventsHandler.currentWalkingSpeed);
        movementType = MovementType.Step;
        fmod_footstepEvent.setParameterByName("MovementType", (int)movementType);
        fmod_footstepEvent.setParameterByName("FloorType", (int)floorType);
        fmod_footstepEvent.setParameterByName("ShoeType", (int)shoeType);

        RESULT result = fmod_footstepEvent.start();
        //UnityEngine.Debug.Log(result.ToString());
    }

    void DoFootscuff()
    {

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

        fmod_clothingrustleEvent.setParameterByName("ClothingSpeed", 1f);
        fmod_clothingrustleEvent.start();
        UnityEngine.Debug.Log("valid? " + fmod_clothingrustleEvent.isValid());

        fmod_clothingrustleEvent.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE stateThing);
        UnityEngine.Debug.Log("state? " + stateThing.ToString());
    }

    void DoClothingRustleStop()
    {
        UnityEngine.Debug.Log("shoul dstop");
        fmod_clothingrustleEvent.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
    }
}
