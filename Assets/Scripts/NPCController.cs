using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPCController : MonoBehaviour
{
    public Transform targetTransform;
    public float marginTransform;
    public float speed;

    private float timeToChangeDirection;

    private bool isWalking;
    private Animator animationComponent;
    private bool blocking;
    private bool changingDirection;
    private int direction;
    private int timesRotated;

    private int thingsInCollider;

    // Use this for initialization
    public void Start()
    {
        animationComponent = GetComponent<Animator>();
        isWalking = true;
        blocking = false;
        changingDirection = true;
        thingsInCollider = 1;
    }

    // Update is called once per frame
    public void Update()
    {
        if (changingDirection)
        {
            if (direction == 0)
            {
                direction = Random.Range(-180, 180);
            }
            else if (timesRotated == direction)
            {
                if (thingsInCollider <= 0)
                {
                    changingDirection = false;
                    direction = 0;
                    timesRotated = 0;
                }
                else
                {
                    direction = Random.Range(-180, 180);
                    timesRotated = 0;
                }
            }
            else if (direction < 0)
            {
                transform.Rotate(Vector3.down, 1);
                timesRotated--;
            }
            else if (direction > 0)
            {
                transform.Rotate(Vector3.up, 1);
                timesRotated++;
            }
        }
        if (blocking)
        {
            if (!checkDoubleInRange(this.transform.position.x, targetTransform.position.x - marginTransform, targetTransform.position.x + marginTransform) && !checkDoubleInRange(this.transform.position.z, targetTransform.position.z - marginTransform, targetTransform.position.z + marginTransform))
            {
                //animationComponent.Play("Walking");
                animationComponent.SetInteger("AnimationState", 0);
                this.transform.LookAt(targetTransform);
                this.GetComponent<Rigidbody>().velocity = transform.forward * speed;
            }
            else
            {
                StandStillLookAtCamera();
            }
        }
        else
        {
            if (isWalking)
            {
                //animationComponent.Play("Walking");
                animationComponent.SetInteger("AnimationState", 0);
                timeToChangeDirection -= Time.deltaTime;

                if (timeToChangeDirection <= 0)
                {
                    timeToChangeDirection = 5;
                    changingDirection = true;
                }

                this.GetComponent<Rigidbody>().velocity = transform.forward * speed;
            }
            else
            {
                StandStillLookAtCamera();
            }
        }
        //Debug.DrawRay(this.transform.position, Camera.main.transform, Color.red);
    }

    private void StandStillLookAtCamera()
    {
        //animationComponent.Play("AngryStandingStill");
        animationComponent.SetInteger("AnimationState", 2);

        this.GetComponent<Rigidbody>().velocity = new Vector3();

        Vector3 eulerBefore = this.transform.eulerAngles;
        this.transform.LookAt(Camera.main.transform);
        eulerBefore.y = this.transform.eulerAngles.y;
        this.transform.eulerAngles = eulerBefore;
    }

    void OnCollisionEnter(Collision col)
    {
        changingDirection = true;
        thingsInCollider ++;
    }

    private void OnCollisionExit(Collision collision)
    {
        thingsInCollider --;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ears")
        {
            isWalking = false;
        }
        thingsInCollider ++;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ears")
        {
            isWalking = true;
            changingDirection = true;
        }
        thingsInCollider--;
    }

    public void blockingSwitch()
    {
        if (blocking)
        {
            blocking = false;
        }
        else
        {
            blocking = true;
        }
    }

    private bool checkDoubleInRange(double value, double min, double max)
    {
        if (value <= max && value >= min)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}