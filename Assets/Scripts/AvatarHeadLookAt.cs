using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvatarHeadLookAt : MonoBehaviour
{
    public Transform head;
    public Transform neck;
    public float maxAngle;
    public float headRotationSpeed;

    public void LookAt(Transform target)
    {
        Quaternion newHeadLookRotation = Quaternion.LookRotation(target.position - head.position);

        if (Quaternion.Angle(neck.rotation, newHeadLookRotation) < maxAngle)
        {
            head.rotation = Quaternion.Lerp(head.rotation, newHeadLookRotation, Time.deltaTime * headRotationSpeed);
        }
        else
        {
            head.rotation = Quaternion.Lerp(head.rotation, neck.rotation, Time.deltaTime * headRotationSpeed);
        }
    }
}
