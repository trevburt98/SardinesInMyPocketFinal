using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class ParkerPlayerSpawner : MonoBehaviour
{
    [SerializeField]
    GameObject playerPrefab;

    [SerializeField]
    Transform player1SpawnPosition;
    [SerializeField]
    Transform player2SpawnPosition;

    private GameObject player1;
    private GameObject player2;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            player1 = PhotonNetwork.Instantiate(playerPrefab.name, player1SpawnPosition.position, Quaternion.identity, 0);
            player1.transform.name = "Player1";
        }
        else
        {
            Debug.Log("Spawning second player");
            player2 = PhotonNetwork.Instantiate(playerPrefab.name, player2SpawnPosition.position, Quaternion.identity, 0);
            player2.transform.name = "Player2";
        }
    }
}
