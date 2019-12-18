using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class ParkerDoorBehaviour : MonoBehaviour, IOnEventCallback
{
    [Header("Door Settings")]
    public int openSpeed = 1;
    public bool unlocked = false;
    //public GameObject openSwitch;
    public AudioClip doorSound;

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

    //private ParkerDoorSwitch doorSwitch;        //(Script attached to Switch that unlocks door)
    private bool playSound = false;

    private byte openEvent = 1;
    private byte closeEvent = 2;
    private bool isActivated = false;

    void Start()
    {
        //Grab script from openSwitch gameObject to access it's methods/variables
        //doorSwitch = (ParkerDoorSwitch) openSwitch.GetComponent(typeof(ParkerDoorSwitch));

        GetComponent<AudioSource> ().playOnAwake = false;
        GetComponent<AudioSource> ().clip = doorSound;
        
    }

    void Update()
    {
        if (isActivated)
        {
            OpenDoors();
        }
        else
        if (!isActivated)
        {
            CloseDoors();
        }
    }

    public void OpenDoors()
    {
        if (playSound == false) //Play sound once
        {
            GetComponent<AudioSource> ().Play ();
            playSound = true;
        }

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
        if (playSound == true) //Play sound once
        {
            GetComponent<AudioSource> ().Play ();
            playSound = false;
        }

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

    public void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnDisable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }

    public void OnEvent(EventData photonEvent)
    {
        byte eventCode = photonEvent.Code;

        if(eventCode == openEvent)
        {
            object[] data = (object[])photonEvent.CustomData;

            string[] doorList = (string[])data[0];

            for(int i = 0; i < doorList.Length; i++)
            {
                if (this.transform.name == doorList[i])
                {
                    isActivated = true;
                }
            }
        }

        if(eventCode == closeEvent)
        {
            object[] data = (object[])photonEvent.CustomData;

            string[] doorList = (string[])data[0];

            for (int i = 0; i < doorList.Length; i++)
            {
                if (this.transform.name == doorList[i])
                {
                    isActivated = false;
                }
            }
        }
    }
}
