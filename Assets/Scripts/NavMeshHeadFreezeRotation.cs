using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshHeadFreezeRotation : MonoBehaviour
{
    public Transform parentTransform;

    void LateUpdate()
    {
        float yRot = parentTransform.rotation.eulerAngles.y;
        Vector3 newRot = Quaternion.identity.eulerAngles;
        
        newRot.y = yRot;
        transform.rotation = Quaternion.Euler(newRot); 
    }
}
