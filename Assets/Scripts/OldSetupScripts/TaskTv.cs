using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaskTv : MonoBehaviour {

    public bool taskComplete;
    public TaskManager clipBoard;
    public string task;
    public GameObject TvScreen;

    private bool isCompleting = false;
    private bool timerRunning = false;
    private bool taskGoalCollider;


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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Controller")
        {
            SwitchTv();
        }
    }

    public void SwitchTv()
    {
        if (TvScreen.activeSelf)
        {
            TvScreen.SetActive(false);
            isCompleting = true;
        }
        else
        {
            TvScreen.SetActive(true);
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

    public bool checkTv()
    {
        if(TvScreen.activeSelf == true)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
