using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    private int roomNumber;
    private bool loaded = false;

    public string firstLevel;
    public Text text;
    public GameObject createButton;
    public GameObject joinButton;
    public GameObject notification;
    public GameObject errorText;
    public GameObject backButton;

    public void createNewRoomWithRandomNumber()
    {
        roomNumber = Random.Range(0, 100);     
        text.text = "Your room number: " + roomNumber;
        createButton.SetActive(false);
        joinButton.SetActive(false);
        backButton.SetActive(true);
        backButton.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 0, 0);

        createAndJoinRoom();
    }

    public void createAndJoinRoom()
    {
        errorText.SetActive(false);
        string roomName = "SardinesInMyPocket" + roomNumber;
        if (PhotonNetwork.IsConnectedAndReady)
        {
            PhotonNetwork.CreateRoom(roomName, new RoomOptions { MaxPlayers = 2 }, new TypedLobby());
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created room " + PhotonNetwork.CurrentRoom);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError(message);
        errorText.GetComponent<Text>().text = "Creating lobby failed, please try again";
        errorText.SetActive(true);
    }

    public void Update()
    {
        if(PhotonNetwork.InRoom)
        {
            if(PhotonNetwork.PlayerList.Length == 2 && !loaded)
            {
                notification.GetComponent<Text>().text = "Starting Game";
                notification.SetActive(true);
                PhotonNetwork.LoadLevel(firstLevel);
                loaded = true;
            } else
            {
                notification.GetComponent<Text>().text = "Waiting for Player 2 to join";
                notification.SetActive(true);
            }
        }
    }
}
