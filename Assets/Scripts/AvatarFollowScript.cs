using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatarFollowScript : MonoBehaviour
{
    public NavMeshAgent avatarNavMesh;
    public Transform playerPosition;
    public Transform playerTargetPosition;

    public float distanceSensitivity = 0.45f;
    public float distanceRotationSensitivity = 0.45f;
    public float rotationSpeed = 1f;

    private void Update()
    {
        if (Vector3.Distance(avatarNavMesh.transform.position, playerTargetPosition.position) > distanceSensitivity)
        {
            avatarNavMesh.destination = playerTargetPosition.position;
        }
    }

    private void LateUpdate()
    {
        if (Vector3.Distance(avatarNavMesh.transform.position, playerTargetPosition.position) < distanceRotationSensitivity)
        {
            Vector3 relativePos = playerPosition.position - playerTargetPosition.transform.position;
            Quaternion LookAtRotation = Quaternion.LookRotation(relativePos);
            Quaternion LookAtRotationOnly_Y = Quaternion.Euler(transform.rotation.eulerAngles.x, LookAtRotation.eulerAngles.y, transform.rotation.eulerAngles.z);

            avatarNavMesh.transform.rotation = Quaternion.Lerp(avatarNavMesh.transform.rotation, LookAtRotationOnly_Y, rotationSpeed);
        }
    }
}
