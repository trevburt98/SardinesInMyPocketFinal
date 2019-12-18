using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using UnityEngine.Video;

public class ObamaDoor : MonoBehaviour, IOnEventCallback
{
    public Vector3 doorPosition;
    public List<GameObject> videoPlayerList;

    private bool twoBool = false;
    private bool oneBool = false;
    private bool complete = false;

    private byte twoPressed = 82;
    private byte twoUnPressed = 83;
    private byte onePressed = 84;
    private byte oneUnPressed = 85;

    void Update()
    {
        if (!complete)
        {
            if (twoBool && oneBool)
            {
                complete = true;
                openDoor();
                foreach(GameObject video in videoPlayerList)
                {
                    VideoPlayer videoPlayer = video.GetComponent<VideoPlayer>();
                    videoPlayer.Pause();
                }
            }
            else
            {
                closeDoor();
            }
        }
    }

    void openDoor()
    {
        this.transform.position = (new Vector3(0, -900, 0));
    }

    void closeDoor()
    {
        this.transform.position = doorPosition;
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
        byte evCode = photonEvent.Code;
        
        if(evCode == twoPressed)
        {
            twoBool = true;
        }

        if(evCode == twoUnPressed)
        {
            twoBool = false;
        }

        if(evCode == onePressed)
        {
            oneBool = true;
        }

        if(evCode == oneUnPressed)
        {
            oneBool = false;
        }
    }
}
