using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class DoorControl : MonoBehaviour
{
    public GameObject goodRotation;

    private CircularDrive drive;
    public bool closing;
    public float timeToClose;

    // Use this for initialization
    void Start()
    {
        closing = false;
        drive = GetComponent<CircularDrive>();
    }

    // Update is called once per frame
    void Update()
    {
        if (closing)
        {
            
            //transform.rotation = Quaternion.Slerp(transform.rotation, new Quaternion(0, 90, 0, 0), timeToClose);
            transform.rotation = Quaternion.Slerp(transform.rotation, goodRotation.transform.rotation, timeToClose);
            if (transform.rotation.eulerAngles.y >=89 && transform.rotation.eulerAngles.y <= 91)
            {
                closing = false;
                this.GetComponent<CircularDrive>().outAngle = 90;
            }

        }
    }

    public void closeDoor()
    {
        closing = true;
    }
}
