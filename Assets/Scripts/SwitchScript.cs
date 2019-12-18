using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class SwitchScript : MonoBehaviour
{
    [SerializeField]
    Material offMaterial;

    [SerializeField]
    Material onMaterial;

    public bool key1;
    public bool key2;

    public bool switchActive;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().material = offMaterial;
        switchActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider other)
    {
        gameObject.GetComponent<MeshRenderer>().material = onMaterial;
        switchActive = true;
        if (key1)
        {
            sendOnePressed();
        } else if(key2)
        {
            sendTwoPressed();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        gameObject.GetComponent<MeshRenderer>().material = offMaterial;
        switchActive = false;
        if (key1)
        {
            sendOneUnPressed();
        } else if (key2)
        {
            sendTwoUnPressed();
        }
    }

    private void sendOnePressed()
    {
        byte evCode = 84;
        object[] content = new object[] { };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
    }

    private void sendOneUnPressed()
    {
        byte evCode = 85;
        object[] content = new object[] { };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
    }

    private void sendTwoPressed()
    {
        byte evCode = 82;
        object[] content = new object[] { };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
    }

    private void sendTwoUnPressed()
    {
        byte evCode = 83;
        object[] content = new object[] { };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        ExitGames.Client.Photon.SendOptions sendOptions = new ExitGames.Client.Photon.SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(evCode, content, raiseEventOptions, sendOptions);
    }
}
