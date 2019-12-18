using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class OpenDoor : MonoBehaviour, IOnEventCallback
{
   

    private byte openEvent = 3;

    private bool open;
    // Start is called before the first frame update
    void Start()
    {
        open = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (open)
        {
            gameObject.GetComponent<Transform>().localPosition = new Vector3(100f, 6.9f, -13.3f);
        }
        else
        {
            gameObject.GetComponent<Transform>().localPosition = new Vector3(-2.9f, 6.9f, -13.3f);
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
        
        if (eventCode == openEvent)
        {
            object[] data = (object[])photonEvent.CustomData;
            open = true;
        }
    }
}
