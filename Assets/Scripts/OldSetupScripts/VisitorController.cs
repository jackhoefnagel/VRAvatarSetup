using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorController : MonoBehaviour
{
    public float speed;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.GetComponent<Rigidbody>().velocity = transform.forward * speed;
    }
}
