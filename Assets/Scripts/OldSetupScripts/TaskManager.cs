using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskManager : MonoBehaviour
{

    public bool AlltaskComplete;

    private List<string> tasks;
    private Text textMesh;

    // Use this for initialization
    void Start()
    {
        if (tasks == null)
        {
            tasks = new List<string>();
        }
        if (textMesh == null)
        {
            textMesh = GetComponentInChildren<Text>();
            textMesh.text = "Tasks:";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void UpdateTextOnClipboard()
    {
        if (textMesh == null)
        {
            textMesh = GetComponentInChildren<Text>();
        }
        textMesh.text = "Tasks: \n";

        foreach (string task in tasks)
        {
            textMesh.text = textMesh.text + task;
        }

    }

    public void giveTask(string task)
    {
        if (tasks == null)
        {
            tasks = new List<string>();
        }
        tasks.Add(task + "\n");
        UpdateTextOnClipboard();

    }

    public int checkTaskProgress()
    {
        return tasks.Count;
    }

    public void completeTask(string task)
    {
        Debug.Log("trying to complete task: " + task);
        if (tasks.Contains(task + "\n"))
        {
            tasks.Remove(task + "\n");
            UpdateTextOnClipboard();
        }

        else
        {
            Debug.Log("Task couldnt be found:" + task);
        }
    }
}
