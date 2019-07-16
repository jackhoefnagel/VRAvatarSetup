using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    public GameObject patient;
    public Button btnPeopleHallway;
    public Button btnResetScene;
    public Button btnBlockDoor;
    public Button btnCloseDoor;
    public Button btnSwitchTv;
    public bool peopleHallway;
    public GameObject visitorsSpawner;
    public GameObject Malcom;

    // Use this for initialization
    void Start()
    {
        peopleHallway = true;
        btnPeopleHallway.onClick.AddListener(TaskOnClickPeopleHallway);
        btnResetScene.onClick.AddListener(OnClickResetScene);
        btnBlockDoor.onClick.AddListener(Malcom.GetComponent<MalcolmActions>().act_BlockDoor);
        btnCloseDoor.onClick.AddListener(Malcom.GetComponent<MalcolmActions>().act_CloseDoor);
        btnSwitchTv.onClick.AddListener(Malcom.GetComponent<MalcolmActions>().act_TurnTV);
        Text text = btnPeopleHallway.GetComponentInChildren<Text>();
        text.text = "Mensen op gang: ja";
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void TaskOnClickPeopleHallway()
    {
        Text text = btnPeopleHallway.GetComponentInChildren<Text>();
        if (peopleHallway)
        {
            peopleHallway = false;
            text.text = "Mensen op gang: nee";
            visitorsSpawner.SetActive(false);
        }
        else
        {
            peopleHallway = true;
            text.text = "Mensen op gang: ja";
            visitorsSpawner.SetActive(true);
        }
    }

    private void OnClickResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnClickBlockDoor()
    {
        NPCController script = patient.GetComponent<NPCController>();
        script.blockingSwitch();
    }
}

