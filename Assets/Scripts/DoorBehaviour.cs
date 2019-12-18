using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorBehaviour : MonoBehaviour
{
    [Header("Door Settings")]
    public int openSpeed = 1;
    public bool unlocked = false;
    public GameObject openSwitch;

    [Header("Door Objects")]
    [Tooltip("Left Door GameObject")]
    public GameObject ld;

    [Tooltip("Right Door GameObject")]
    public GameObject rd;

    [Header("Door State Positions")]
    public GameObject ldOpen;           //ld Open Position
    public GameObject rdOpen;           //rd Open Position
    public GameObject ldClose;          //ld Close Position
    public GameObject rdClose;          //rd Close Position

    private DoorSwitch doorSwitch;        //(Script attached to Switch that unlocks door)

    void Start()
    {
        //Grab script from openSwitch gameObject to access it's methods/variables
        doorSwitch = (DoorSwitch) openSwitch.GetComponent(typeof(DoorSwitch));
    }

    void Update()
    {
        if (doorSwitch.IsActivated())
        {
            OpenDoors();
        } else
        if (!doorSwitch.IsActivated())
        {
            CloseDoors();
        }
    }

    public void OpenDoors()
    {
        float currentSpeed = openSpeed * Time.deltaTime;

        //Move doors to open position
        ld.transform.position = Vector3.MoveTowards(ld.transform.position, ldOpen.transform.position, currentSpeed);
        rd.transform.position = Vector3.MoveTowards(rd.transform.position, rdOpen.transform.position, currentSpeed);

        if (Vector3.Distance(ld.transform.position, ldOpen.transform.position) < 0.001f) {
            currentSpeed = 0;
        }

        if (Vector3.Distance(rd.transform.position, rdOpen.transform.position) < 0.001f) {
            currentSpeed = 0;
        }
    }

    public void CloseDoors()
    {
        float currentSpeed = openSpeed * Time.deltaTime;

        //Move doors to close position
        ld.transform.position = Vector3.MoveTowards(ld.transform.position, ldClose.transform.position, currentSpeed);
        rd.transform.position = Vector3.MoveTowards(rd.transform.position, rdClose.transform.position, currentSpeed);

        if (Vector3.Distance(ld.transform.position, ldClose.transform.position) < 0.001f) {
            currentSpeed = 0;
        }

        if (Vector3.Distance(rd.transform.position, rdClose.transform.position) < 0.001f) {
            currentSpeed = 0;
        }
    }
}
