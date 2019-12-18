using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ConnectToRoom : MonoBehaviourPunCallbacks
{

    [SerializeField]
    string roomName;
    [SerializeField]
    string firstSceneName;

    void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions { MaxPlayers = 2 }, new TypedLobby());
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room with name " + roomName);
    }

    public void loadFirstLevel()
    {
        PhotonNetwork.LoadLevel(firstSceneName);
    }
}
