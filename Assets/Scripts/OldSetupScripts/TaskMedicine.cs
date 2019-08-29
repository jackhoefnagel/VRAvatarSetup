using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskMedicine : MonoBehaviour
{
    public bool taskComplete;
    public GameObject taskTargetObject;
    public TaskManager clipBoard;
    public string task;

    private GameObject taskGoalCollider;
    private bool isCompleting = false;
    private bool timerRunning = false;
    

    // Use this for initialization
    void Start()
    {
        taskGoalCollider = GetComponent<Collider>().gameObject;
        clipBoard.giveTask(task);
    }

    // Update is called once per frame
    void Update()
    {
        if (isCompleting && !timerRunning && !taskComplete)
        {
            StartCoroutine(timer());
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject == taskTargetObject)
        {
            isCompleting = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.tag == taskTargetObject.tag)
        {
            isCompleting = false;
        }
    }

    private IEnumerator timer()
    {
        timerRunning = true;
        yield return new WaitForSeconds(3);
        if (isCompleting)
        {
            taskComplete = true;
            endTask();
        }
        timerRunning = false;
    }

    private void endTask()
    {
        //TODO give new task or something.
        clipBoard.completeTask(task);
    }
}
