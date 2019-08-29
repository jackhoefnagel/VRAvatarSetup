// Prints the name of the object camera is directly looking at
using UnityEngine;
using System.Collections;

public class RayCam : MonoBehaviour
{
    public GameObject cam;

    public void Start()
    {

    }

    void Update()
    {
       // Ray ray = new Ray(this.transform.position, cam.transform.position);
        Debug.DrawRay(this.transform.position, cam.transform.position, Color.red);
        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, cam.transform.position, out hit,8))
            if(hit.collider.tag == "MainCamera")
            {
                print("I'm looking at " + hit.transform.name);
            }
        else
            print("I'm looking at nothing!");
    }
}
