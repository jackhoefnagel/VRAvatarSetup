using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AvatarFollowScript : MonoBehaviour
{
    public NavMeshAgent avatarNavMesh;
    public Transform playerPosition;

    private void Update()
    {
        avatarNavMesh.destination = playerPosition.position;
    }
}
