using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD;

public class FMODunityHandler : MonoBehaviour
{
    public AvatarAnimationEventsHandler avatarAnimationEventsHandler;

    [FMODUnity.EventRef]
    public string footstepEvent;
    public FMOD.Studio.EventInstance fmod_footstepEvent;
    public FMODUnity.StudioEventEmitter footstepEventEmitter;

    [Header("Footstep Variables")]
    public Transform feetSoundPropagationPosition;
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




    private void OnEnable()
    {
        avatarAnimationEventsHandler.footstep.AddListener(DoFootstep);
        avatarAnimationEventsHandler.footscuff.AddListener(DoFootscuff);

        fmod_footstepEvent = footstepEventEmitter.EventInstance;

        /*
        for (int i = 0; i < ambienceObjects.Count; i++)
        {
            ambienceObjects[i].EventInstance.setParameterByName("WindDirection", i);
            ambienceObjects[i].EventInstance.start();
        }
        */
    }

    private void OnDisable()
    {
        avatarAnimationEventsHandler.footstep.RemoveListener(DoFootstep);
    }

    void DoFootstep()
    {

        fmod_footstepEvent.setParameterByName("WalkingSpeed", avatarAnimationEventsHandler.currentWalkingSpeed);
        movementType = MovementType.Step;
        fmod_footstepEvent.setParameterByName("MovementType", (int)movementType);
        fmod_footstepEvent.setParameterByName("FloorType", (int)floorType);
        fmod_footstepEvent.setParameterByName("ShoeType", (int)shoeType);

        fmod_footstepEvent.start();

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
}
