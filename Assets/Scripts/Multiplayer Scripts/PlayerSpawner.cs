using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    Vector3 player1SpawnPosition;
    [SerializeField]
    Vector3 player2SpawnPosition;

    private GameObject player1;
    private GameObject player2;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            player1 = PhotonNetwork.Instantiate(playerPrefab.name, player1SpawnPosition, Quaternion.identity, 0);
            player1.transform.name = "Player1";
        }
        else
        {
            Debug.Log("Spawning second player");
            player2 = PhotonNetwork.Instantiate(playerPrefab.name, player2SpawnPosition, Quaternion.identity, 0);
            player2.transform.name = "Player2";
        }
    }

    public void Respawn(int playerNumber)
    {
        if(playerNumber == 1)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(player1);
                player1 = PhotonNetwork.Instantiate(playerPrefab.name, player1SpawnPosition, Quaternion.identity, 0);
                player1.transform.name = "Player1";
            }
        } else
        {
            if(!PhotonNetwork.IsMasterClient)
            {
                PhotonNetwork.Destroy(player2);
                player2 = PhotonNetwork.Instantiate(playerPrefab.name, player2SpawnPosition, Quaternion.identity, 0);
                player2.transform.name = "Player2";
            }
        }
    }
}
