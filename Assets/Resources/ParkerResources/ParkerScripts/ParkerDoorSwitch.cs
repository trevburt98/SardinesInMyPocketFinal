using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ParkerDoorSwitch : MonoBehaviour
{
    [Header("Switch Settings")]
    public List<GameObject> doorList;
    [Tooltip("GameObject must have 'Key' tag")]
    public bool needKey = false;
    public bool isActivated = false;

    [Tooltip("Switch Automatically locks")]
    public bool pressurePlate = true;
    public Material onColor;
    public Material offColor;

    private Renderer color;
    private RaycastHit hitInfo;
    private Vector3 above;
    private int playerLayer = 8; //Layer that players use


    //To make switch/button clickable, switch it's layer to 'Switches' Layer
    //and uncheck 'pressurePlate' in Unity Inspector. 
    //Check Player 'pickup' script to see how it detects switch/button


    void Start()
    {
        color = gameObject.GetComponent<Renderer>();
    }

    void Update()
    {
        if (isActivated) {
            color.material = onColor;
        } else {
            color.material = offColor;
        }

        //Shoot raycast up to see if player is on switch
        Debug.DrawRay(gameObject.transform.position, above * 2, Color.red);
        above = gameObject.transform.TransformDirection(Vector3.up);
        if (Physics.Raycast(gameObject.transform.position, above, out hitInfo, 2))
        {
            if (hitInfo.transform.gameObject.layer == playerLayer)
            {
                if (!needKey && hitInfo.transform.gameObject.tag == "Player") {
                    isActivated = true;
                    sendOpen();
                } else if (pressurePlate) {
                    isActivated = false;
                    sendClosed();
                }
            }
        }
/*        else
        {
            isActivated = false;
            if(doorList[0].transform.Find("LeftDoor").GetComponent<Transform>().position != doorList[0].transform.Find("LeftDoorClosePos").GetComponent<Transform>().position)
            {
                sendClosed();
            }
        }*/
    }

    public bool IsActivated()
    {
        return isActivated;
    }

    public void toggleSwitch() {
        Debug.Log("toggling switch");
        if (isActivated) {
            isActivated = false;
        }
        else {
            isActivated = true;
            sendOpen();
        }
    }

    public void OnCollisionEnter(Collision c)
    {
        Debug.Log("entering collision");
        if (!needKey) {
            isActivated = true;
            sendOpen();
        } else if(c.gameObject.tag == "Key") {
            isActivated = true;
            sendOpen();
        }
    }

    public void OnCollisionExit(Collision c)
    {
        Debug.Log("exiting collision");
        if (pressurePlate)
        {
            isActivated = false;
            sendClosed();
        }
    }

    public void sendOpen()
    {
        string[] doorNames = new string[doorList.Capacity];

        for(int i = 0; i < doorList.Capacity; i++)
        {
            doorNames[i] = doorList[i].transform.name;
        }

        byte evCode = 1;
        object[] content = new object[] { doorNames };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
    }

    public void sendClosed()
    {
        string[] doorNames = new string[doorList.Capacity];

        for (int i = 0; i < doorList.Capacity; i++)
        {
            doorNames[i] = doorList[i].transform.name;
        }

        byte evCode = 2;
        object[] content = new object[] { doorNames };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
    }
}
