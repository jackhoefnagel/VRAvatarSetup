using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisitorSpawner : MonoBehaviour {
    public GameObject spawner1;
    public GameObject spawner2;

    public GameObject visitor1;
    public GameObject visitor2;
    public GameObject visitor3;

    private bool currentSpawnedVis1 = false;
    private bool currentSpawnedVis2 = false;
    private bool currentSpawnedVis3 = false;

    private GameObject currentSpawner;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        float randomSpawnFloat = Random.Range(-1f,1f);
        int randomDirection = Random.Range(0, 2);

        if(randomDirection == 1)
        {
            currentSpawner = spawner1;
            visitor1.transform.eulerAngles = new Vector3(0, -90, 0);
            visitor2.transform.eulerAngles = new Vector3(0, -90, 0);
            visitor3.transform.eulerAngles = new Vector3(0, -90, 0);
        }
        else
        {
            currentSpawner = spawner2;
            visitor1.transform.eulerAngles = new Vector3(0, 90, 0);
            visitor2.transform.eulerAngles = new Vector3(0, 90, 0);
            visitor3.transform.eulerAngles = new Vector3(0, 90, 0);
        }

        if (!currentSpawnedVis1)
        {
            GameObject test = Instantiate(visitor1, currentSpawner.transform);
            test.transform.localPosition = new Vector3(0, 0, randomSpawnFloat);
            currentSpawnedVis1 = true;
        }
        else if (!currentSpawnedVis2)
        {
            GameObject test = Instantiate(visitor2, currentSpawner.transform);
            test.transform.localPosition = new Vector3(0, 0, randomSpawnFloat);
            currentSpawnedVis2 = true;
        }
        else if (!currentSpawnedVis3)
        {
            GameObject test = Instantiate(visitor3, currentSpawner.transform);
            test.transform.localPosition = new Vector3(0, 0, randomSpawnFloat);
            currentSpawnedVis3 = true;
        }
    }

    public void VisitorDestroyed(int nr)
    {
        if(nr == 1)
        {
            currentSpawnedVis1 = false;
        }
        else if(nr == 2)
        {
            currentSpawnedVis2 = false;
        }
        else if(nr == 3)
        {
            currentSpawnedVis3 = false;
        }
    }
}
