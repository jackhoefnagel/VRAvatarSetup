using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AI;


public class ClickMove : MonoBehaviour
{
    public Camera clickMoveCamera;
    public NavMeshAgent characterToMove;
    public AvatarStateController avatarStateController;

    void Update()
    {
        RaycastHit hit;
        Ray ray = clickMoveCamera.ScreenPointToRay(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                if (!avatarStateController.avatarAnimator.GetBool("sit"))
                {
                    characterToMove.destination = hit.point;
                }
            }
        }
    }
}
