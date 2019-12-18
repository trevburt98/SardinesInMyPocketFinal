using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ObamaRoomScript : MonoBehaviour
{
    public void loadNewLevel(string levelName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Changing Levels");
            GameObject player1 = GameObject.Find("Player1");
            player1.GetPhotonView().RPC("Destroy", RpcTarget.All);
            PhotonNetwork.LoadLevel(levelName);
        }
    }
}
