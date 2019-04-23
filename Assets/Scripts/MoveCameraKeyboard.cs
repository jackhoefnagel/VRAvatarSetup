using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraKeyboard : MonoBehaviour
{
    private int speed = 10;

    public void Move()
    {

        Vector3 Movement = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        transform.Translate(Movement * speed * Time.deltaTime);
    }

    void Update()
    {
        Move();
    }
}
