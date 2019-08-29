using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFollowCamera : MonoBehaviour {

    public GameObject camera;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        this.transform.LookAt(camera.transform);
	}
}
