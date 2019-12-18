using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class CreateNewRoom : MonoBehaviour
{
    public int roomNumber;
    public Text textBox;

    public void createRoomWithRandomNumber()
    {
        roomNumber = Random.Range(0, 100);
        textBox.text = "Tell your friends to connect with " + roomNumber;
        string roomName = "SardinesInMyPocket" + roomNumber;

        if(PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 2 }, new TypedLobby());
        }
    }
}
