using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    public GameObject enemy;

    public float X = 50f;
    public float Y = 30f;
    public float Z;
    public float spawnPointZ;
    private GameObject player1;


    public float spawnTime = 1f;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            print("I am master " + PhotonNetwork.CountOfPlayers);
            InvokeRepeating("Spawn", spawnTime, spawnTime);
        }
    }

    // Update is called once per frame
    void Update()
    {
      
    }
   
    void Spawn()
    {
        float spawnPointX = X;
        float spawnPointY = Y;
        Z = Random.Range(-13, 15);
        Vector3 spawnPosition = new Vector3(spawnPointX, spawnPointY, Z);
        PhotonNetwork.Instantiate(enemy.name, spawnPosition, Quaternion.identity);
        
    }
    public void LeaveRoom(string levelName)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            player1 = GameObject.Find("Player1");
            player1.GetPhotonView().RPC("Destroy", RpcTarget.All);
            PhotonNetwork.LoadLevel(levelName);
        }
    }

}
