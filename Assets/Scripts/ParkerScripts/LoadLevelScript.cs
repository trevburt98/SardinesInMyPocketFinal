using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class LoadLevelScript : MonoBehaviour
{
    private GameObject player1;
    private GameObject player2;
    public void changeLevel(string levelName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            Debug.Log("Changing Levels");
            player1 = GameObject.Find("Player1");
            player1.GetPhotonView().RPC("Destroy", RpcTarget.All);
            PhotonNetwork.LoadLevel(levelName);
        }
    }
}
