using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MalcolmActions : MonoBehaviour
{

    public Transform[] _goals;
    public Transform _replace;
    public GameObject _player;
    public GameObject _door;
    public GameObject _tvButton;
    public int _rage = 0;

    private bool assault = false;

    void Start()
    {
        act_Random();
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject == _player)
        {
            if (assault)
            {
                assault = false;
                GetComponent<MalcomController>()._agent.ResetPath();
            }
            else if (_rage == 0)
            {
                act_BackingOff();
            }
            else
            {
                transform.LookAt(_player.transform);
            }
        }
    }

    void act_Random()
    {
        int _locations = Random.Range(0, 3);
        GetComponent<MalcomController>()._destination = _goals[_locations];
        GetComponent<MalcomController>()._act = _locations;
        if (_locations == 2)
        {
            GetComponent<MalcomController>()._act = 0;
        }
        GetComponent<MalcomController>()._agentAct();
    }

    public void act_BackingOff()
    {
        _rage += 1;
        transform.LookAt(_player.transform);
        _replace.position = transform.position + (transform.position - _player.transform.position);
        GetComponent<MalcomController>()._destination = _replace;
        GetComponent<MalcomController>()._agent.updateRotation = false;
        GetComponent<MalcomController>()._agentAct();
    }

    public void act_Assault()
    {
        assault = true;
        transform.LookAt(_player.transform);
        _replace.position = _player.transform.position;
        GetComponent<MalcomController>()._destination = _replace;
        GetComponent<MalcomController>()._agentAct();
    }
    public void act_CloseDoor()
    {
        GetComponent<MalcomController>()._destination = _goals[3];
        GetComponent<MalcomController>()._act = 9;
        GetComponent<MalcomController>()._agentAct();
    }
    public void act_TurnTV()
    {
        GetComponent<MalcomController>()._destination = _goals[0];
        GetComponent<MalcomController>()._act = 8;
        GetComponent<MalcomController>()._agentAct();
    }
    public void act_BlockDoor()
    {
        GetComponent<MalcomController>()._destination = _goals[4];
        GetComponent<MalcomController>()._act = 7;
        GetComponent<MalcomController>()._agentAct();
    }
    public void closeDoor()
    {
        _door.GetComponent<DoorControl>().closeDoor();
    }
    public void SwithTv()
    {
        _tvButton.GetComponent<TaskTv>().SwitchTv();
    }

}