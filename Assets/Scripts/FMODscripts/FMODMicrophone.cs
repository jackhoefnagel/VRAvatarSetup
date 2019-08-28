using UnityEngine;
using System.Collections;
using FMOD;

public class FMODMicrophone : MonoBehaviour
{
    private FMOD.Studio.System sys;

    private void Start()
    {
        sys.initialize(4, FMOD.Studio.INITFLAGS.NORMAL, INITFLAGS.NORMAL, new System.IntPtr(0));
        //sys.get
    }
}
