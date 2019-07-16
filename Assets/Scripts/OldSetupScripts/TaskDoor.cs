using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskDoor : MonoBehaviour
{

    public bool taskComplete;
    public bool playerinHallway;
    public GameObject taskTargetObject;
    public TaskManager clipBoard;
    public string task = "Leave the room.";

    private bool counting;
    private GameObject taskGoalCollider;

    // Use this for initialization
    void Start()
    {
        counting = false;
        playerinHallway = false;
        taskGoalCollider = GetComponent<Collider>().gameObject;
        clipBoard.giveTask(task);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerinHallway && !counting)
        {
            StartCoroutine(wait());
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == taskTargetObject.tag)
        {
            playerinHallway = true;
        }
    }
    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == taskTargetObject.tag)
        {
            playerinHallway = false;
        }
    }

    IEnumerator wait()
    {
        counting = true;
        yield return new WaitForSeconds(3);
        if (playerinHallway && clipBoard.checkTaskProgress() < 2)
        {
            taskComplete = true;
            TaskComplete();
        }
        counting = false;
    }

    private void TaskComplete()
    {
        Debug.Log("Player exited the room.");
        clipBoard.completeTask(task);
    }
}
