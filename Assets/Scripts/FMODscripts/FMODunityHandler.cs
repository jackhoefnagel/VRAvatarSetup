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

    [Header("Footstep Variables")]
    public Transform feetSoundPropagationPosition;
    public float walkSpeedParamFloat;
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


    private void OnEnable()
    {
        avatarAnimationEventsHandler.footstep.AddListener(DoFootstep);

        GameObject feetSoundPropagator = new GameObject("feetSoundPropagator");
        feetSoundPropagator.transform.SetParent(avatarAnimationEventsHandler.transform);
        feetSoundPropagator.transform.localPosition = new Vector3(0, 0, 0);

        fmod_footstepEvent = FMODUnity.RuntimeManager.CreateInstance(footstepEvent);
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(fmod_footstepEvent, feetSoundPropagator.transform, GetComponent<Rigidbody>());
    }

    private void OnDisable()
    {
        avatarAnimationEventsHandler.footstep.RemoveListener(DoFootstep);
    }

    void DoFootstep()
    {

        fmod_footstepEvent.setParameterByName("WalkingSpeed", avatarAnimationEventsHandler.currentWalkingSpeed);
        fmod_footstepEvent.setParameterByName("FloorType", (int)floorType);
        fmod_footstepEvent.setParameterByName("ShoeType", (int)shoeType);

        fmod_footstepEvent.start();

    }
}
