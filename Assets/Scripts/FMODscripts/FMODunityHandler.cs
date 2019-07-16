using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FMODunityHandler : MonoBehaviour
{
    [FMODUnity.EventRef]
    public string footstepEvent;

    [FMODUnity.ParamRef]
    public string walksSpeedParam;
}
