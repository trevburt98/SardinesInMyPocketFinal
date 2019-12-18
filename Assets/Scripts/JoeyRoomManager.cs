using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class JoeyRoomManager : MonoBehaviour
{
    public void spawn(GameObject item, Vector3 pos, int intendedPlayer)
    {
        if (intendedPlayer == 1)
        {
            if (PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(item.name, pos, Quaternion.Euler(0, 180, 0));
            }
        }
        else if (intendedPlayer == 2)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Instantiate(item.name, pos, Quaternion.Euler(0, 180, 0));
            }
        }
        else
        {
            PhotonNetwork.Instantiate(item.name, pos, Quaternion.Euler(0, 180, 0));
        }
    }

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
